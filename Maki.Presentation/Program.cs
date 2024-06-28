using System.Reflection;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using maki_backend.Middleware;
using Maki.Application.Artisan.QueryServices;
using Maki.Application.Customer.CommandServices;
using Maki.Application.Customer.QueryServices;
using Maki.Application.DesignRequest.CommandServices;
using Maki.Application.DesignRequest.QueryServices;
using Maki.Application.IAM.CommandServices;
using Maki.Application.IAM.QueryServices;
using Maki.Application.Product.CommandServices;
using Maki.Application.Product.QueryServices;
using Maki.Domain.Artisan.Repositories;
using Maki.Domain.Artisan.Services;
using Maki.Domain.Customer.Repositories;
using Maki.Domain.Customer.Services;
using Maki.Domain.DesignRequest.Repositories;
using Maki.Domain.DesignRequest.Services;
using Maki.Domain.IAM.Repositories;
using Maki.Domain.IAM.Services;
using Maki.Domain.Product.Repositories;
using Maki.Domain.Product.Services;
using Maki.Infrastructure.Artisan.Persistence;
using Maki.Infrastructure.Customer.Persistence;
using Maki.Infrastructure.DesignRequest.Persistence;
using Maki.Infrastructure.IAM.Persistence;
using Maki.Infrastructure.Product.Persistence;
using Maki.Infrastructure.Shared.Contexts;
using Maki.Presentation.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Management center for Maki",
        Description = "An ASP.NET Core Web API for managing Maki endpoints",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (!File.Exists(xmlFilePath))
    {
        new XDocument(new XElement("Root")).Save(xmlFilePath);
    }
    options.IncludeXmlComments(xmlFilePath);
});

//Dependency injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryCommandService, CategoryCommandService>();
builder.Services.AddScoped<ICategoryQueryService, CategoryQueryService>();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserCommandService,UserCommandService>();
builder.Services.AddScoped<IUserQueryService,UserQueryService>();

builder.Services.AddScoped<IEncryptService,EncryptService>();
builder.Services.AddScoped<ITokenService,TokenService>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerCommandService, CustomerCommandService>();
builder.Services.AddScoped<ICustomerQueryService, CustomerQueryService>();

builder.Services.AddScoped<IArtisanRepository, ArtisanRepository>();
builder.Services.AddScoped<IArtisanQueryService, ArtisanQueryService>();
builder.Services.AddScoped<IArtisanCommandService, ArtisanCommandService>();

builder.Services.AddScoped<IDesignRequestRepository, DesignRequestRepository>();
builder.Services.AddScoped<IDesignRequestCommandService, DesignRequestCommandService>();
builder.Services.AddScoped<IDesignRequestQueryService, DesignRequestQueryService>();

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

//Auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

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

app.UseCors("AllowAllPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<AuthenticationMiddleware>();

app.Run();