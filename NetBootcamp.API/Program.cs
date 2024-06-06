using Bootcamp.Repository;
using Bootcamp.Repository.Identities;
using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Token;
using Bootcamp.Service.Users;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetBootcamp.API.Extensions;
using NetBootcamp.API.Filters;
using System.Reflection;
using System.Text;
using System.Threading.Channels;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(x=> x.Filters.Add<ValidationFilter>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddService(builder.Configuration);
builder.Services.AddScoped<IAuthorizationHandler, OverAgeRequirementHandler>();

builder.Services.AddSingleton(Channel.CreateUnbounded<UserCreatedEvent>());

builder.Services.AddHostedService<BackgroundServiceEmailSender>();

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Over18AgePolicy", x => { x.AddRequirements(new OverAgeRequirement() { Age = 10 }); });


    x.AddPolicy("UpdatePolicy", y => { y.RequireClaim("update2", "true"); });
});


var app = builder.Build();

app.SeedDatabase();

await app.SeedIdentityData();

app.AddMiddlewares();

app.Run();
