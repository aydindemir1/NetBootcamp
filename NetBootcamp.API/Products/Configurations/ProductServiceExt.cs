using FluentValidation;
using NetBootcamp.API.Filters;
using NetBootcamp.API.Products.AsyncMethods;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Products.Helpers;
using NetBootcamp.API.Products.SyncMethods;
using NetBootcamp.API.Repositories;
using System.Runtime.CompilerServices;

namespace NetBootcamp.API.Products.Configurations
{
    public static class ProductServiceExt
    {
        public static void AddProductService(this IServiceCollection services)
        {
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService2, ProductService2>();
            services.AddScoped<IProductRepository2, ProductRepository2>();
           

            services.AddValidatorsFromAssemblyContaining<ProductCreateRequestDto>();
            services.AddScoped<NotFoundFilter>();
            services.AddSingleton<PriceCalculator>();
        }
    }
}
