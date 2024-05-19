﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Products.DTOs;
using NetBootcamp.API.Products.Helpers;
using System.Collections.Immutable;

namespace NetBootcamp.API.Products.SyncMethods
{
    public interface IProductService
    {
        ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax(PriceCalculator priceCalculator);
        ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator);

        ResponseModelDto<ImmutableList<ProductDto>> GetAllByPageWithCalculatedTax(
            PriceCalculator priceCalculator, int page, int pageSize);
        ResponseModelDto<int> Create(ProductCreateRequestDto request);
        ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request);
        ResponseModelDto<NoContent> UpdateProductName(int id, string name);
        ResponseModelDto<NoContent> Delete(int id);



    }
}