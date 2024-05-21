using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.Products.ProductCreateUseCase;
using Bootcamp.Service.Products.SyncMethods;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Bootcamp.Service.Products.Configurations
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
