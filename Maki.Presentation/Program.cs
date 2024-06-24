using maki_backend.Middleware;
using Maki.Application.Customer.CommandServices;
using Maki.Application.Customer.QueryServices;
using Maki.Application.IAM.CommandServices;
using Maki.Application.Product.CommandServices;
using Maki.Application.Product.QueryServices;
using Maki.Domain.Customer.Repositories;
using Maki.Domain.Customer.Services;
using Maki.Domain.IAM.Repositories;
using Maki.Domain.IAM.Services;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;
using Maki.Infrastructure.Customer.Persistence;
using Maki.Infrastructure.IAM.Persistence;
using Maki.Infrastructure.Product.Persistence;
using Maki.Infrastructure.Shared.Contexts;
using Maki.Presentation.Mapper;
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
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserCommandService,UserCommandService>();
builder.Services.AddScoped<IEncryptService,EncryptService>();
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();



//automapper
builder.Services.AddAutoMapper(
    typeof(RequestToModels),
    typeof(ModelsToRequest),
    typeof(ModelsToResponse));

//Conexion a MySQL
var connectionString = builder.Configuration.GetConnectionString("makiConnection");
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<MakiContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

//Generar BD
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