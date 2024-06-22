using maki_backend.Middleware;
using Maki.Application.IAM.CommandServices;
using Maki.Domain.IAM.Repositories;
using Maki.Domain.IAM.Services;
using Maki.Infrastructure.IAM.Persistence;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", 
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency injection
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserCommandService,UserCommandService>();
builder.Services.AddScoped<IEncryptService,EncryptService>();
builder.Services.AddScoped<ITokenService,TokenService>();

//Conexion a MySQL 
var connectionString = builder.Configuration.GetConnectionString("makiConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

builder.Services.AddDbContext<MakiContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<MakiContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllPolicy");

app.Run();