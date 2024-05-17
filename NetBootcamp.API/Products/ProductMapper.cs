﻿using AutoMapper;
using NetBootcamp.API.Products.DTOs;

namespace NetBootcamp.API.Products
{
    public class ProductMapper:Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDto>().ForMember(x => x.Created,
                y => y.MapFrom(y => y.Created.ToShortDateString()))
                .ForMember(x => x.Price, opt => opt.MapFrom(y => new PriceCalculator().CalculateKdv(y.Price,1.20m )));

        }

        
    }
}
