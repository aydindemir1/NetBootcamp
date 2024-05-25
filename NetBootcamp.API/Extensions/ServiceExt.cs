﻿using Bootcamp.Service;
using Bootcamp.Service.Products.Configurations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace NetBootcamp.API.Extensions
{
    public static class ServiceExt
    {
        public static void AddService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(x =>
            {
                x.SuppressModelStateInvalidFilter = true;
            });
            services.AddAutoMapper(typeof(ServiceAssembly).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddProductService();
        }
    }
}
