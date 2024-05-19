using AutoMapper;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Products.Helpers;

namespace NetBootcamp.API.Products.Configurations
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(x => x.Created,
            y => y.MapFrom(y => y.Created.ToShortDateString()))
            .ForMember(x => x.Price, opt => opt.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price, 1.20m)));

        }


    }
}
