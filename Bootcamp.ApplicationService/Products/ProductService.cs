﻿using Bootcamp.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.ApplicationService.Products
{
    public class ProductService(IProductRepository productRepository, ICacheService cacheService)
    {
        

        public async Task<ResponseModelDto<int>> CreateProduct(ProductCreateRequestDto request)
        {
            var productToCreate = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            // save to db
         var product =  await  productRepository.CreateProduct(productToCreate);

            return ResponseModelDto<int>.Success(product.Id);
        }

        public async Task<ResponseModelDto<ProductDto>> GetProductById(int id)
        {
            //cache aside design pattern
            var hasProductAsCache = cacheService.Get<ProductDto?>($"product:{id}");

            if (hasProductAsCache is not null)
            {
                return ResponseModelDto<ProductDto>.Success(hasProductAsCache);
            }

            var product = await productRepository.GetProductById(id);

            if(product is null)
            {
                return ResponseModelDto<ProductDto>.Fail("Not found");
            }

            cacheService.Add($"product:{id}", new ProductDto(product.Id, product.Name, product.Price));

            return ResponseModelDto<ProductDto>.Success(new ProductDto(product.Id,product.Name,product.Price));
        }
    }
}
