using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NetBootcamp.API.Products;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Redis;
using NetBootcamp.API.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(x=> 
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    }
);
// Add services to the container.
builder.Services.AddSingleton<RedisService>(x =>
{
    return new RedisService(builder.Configuration.GetConnectionString("Redis")!);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateRequestDto>();



builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<PriceCalculator>();



var app = builder.Build();

app.SeedDatabase();



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
