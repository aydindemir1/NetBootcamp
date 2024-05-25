﻿using AutoMapper;
using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;


namespace Bootcamp.Service.Products.Configurations
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {

            // Her ikisi de doğru çalışıyor.

            //CreateMap<Product, ProductDto>()
            //.ForMember(x => x.Created,
            //y => y.MapFrom(y => y.Created.ToShortDateString()))
            //.ForMember(x => x.Price, opt => opt.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price, 1.20m)));

            CreateMap<Product, ProductDto>()
           .ForPath(x => x.Created,
           y => y.MapFrom(y => y.Created.ToShortDateString()))
           .ForPath(x => x.Price, opt => opt.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price, 1.20m)));

        }


    }
}
