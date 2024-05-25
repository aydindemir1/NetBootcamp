using Bootcamp.Repository;
using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x=> x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService();


var app = builder.Build();

app.SeedDatabase();

app.AddMiddlewares();

app.Run();
