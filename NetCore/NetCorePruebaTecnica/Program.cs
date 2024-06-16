using TechTest.Configuration;
using TechTest.Services.Interface;
using TechTest.Services;
using TechTestData.Repositories;
using TechTestData.Repositories.Interface;
using TechTestData.Models;
using TechTest.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TechTestData.Data;
using Microsoft.EntityFrameworkCore;
using IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

// Add AutoMapper with a custom mapping profile
builder.Services.AddAutoMapper(typeof(MapperModelToDTO));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<DbContext, DBContext>();
builder.Services.AddRepositories();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:JwtKey"]))
        };
    });

builder.Services.AddDbContext<DBContext>(options => options.UseNpgsql(builder.Configuration["AppSettings:DbConnectionString"]));

builder.Services.AddStackExchangeRedisCache( options => options.Configuration = builder.Configuration["AppSettings:RedisConnectionString"]);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
