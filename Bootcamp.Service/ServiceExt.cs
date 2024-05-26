using Bootcamp.Service;
using Bootcamp.Service.ExceptionHandlers;
using Bootcamp.Service.Products.Configurations;
using Bootcamp.Service.Token;
using Bootcamp.Service.Weather;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetBootcamp.API.Extensions
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddProductService();

            services.AddExceptionHandler<CriticalExceptionHandler>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.Configure<CustomTokenOptions>(configuration.GetSection("TokenOptions"));
            services.Configure<Clients>(configuration.GetSection("Clients"));
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
