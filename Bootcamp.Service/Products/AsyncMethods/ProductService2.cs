using AutoMapper;
using Bootcamp.Repository;
using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.Products.ProductCreateUseCase;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Bootcamp.Service.Products
{
    public class ProductService2(IProductRepository2 productRepository, IUnitOfWork unitOfWork, IMapper mapper) : IProductService2
    {
        public async Task<ResponseModelDto<int>> Create(ProductCreateRequestDto request)
        {
            var newProduct = new Product
            {
                Name = request.Name.Trim(),
                Price = request.Price,
                Stock = 10,
                Barcode = Guid.NewGuid().ToString(),
                Created = DateTime.Now
            };

            await productRepository.Create(newProduct);
            await unitOfWork.CommitAsync();
            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        public async Task<ResponseModelDto<NoContent>> Delete(int id)
        {
            await productRepository.Delete(id);
            await unitOfWork.CommitAsync();
            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        public async Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllByPageWithCalculatedTax(PriceCalculator priceCalculator, int page, int pageSize)
        {
            var productList = await productRepository.GetAllByPage(page, pageSize);


            var productListAsDto = mapper.Map<List<ProductDto>>(productList);

            //var productListAsDto = productList.Select(product => new ProductDto(
            //    product.Id,
            //    product.Name,
            //    priceCalculator.CalculateKdv(product.Price, 1.20m),
            //    product.Created.ToShortDateString()
            //)).ToImmutableList();

            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productListAsDto.ToImmutableList());
        }

        public async Task<ResponseModelDto<ImmutableList<ProductDto>>> GetAllWithCalculatedTax(PriceCalculator priceCalculator)
        {

            var productList = await productRepository.GetAll();

            var productListAsDto = mapper.Map<List<ProductDto>>(productList);


            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productListAsDto.ToImmutableList());
        }

        public async Task<ResponseModelDto<ProductDto?>> GetByIdWithCalculatedTax(int id, PriceCalculator priceCalculator)
        {
            var hasProduct = await productRepository.GetById(id);

            //if (hasProduct is null)
            //{
            //    return ResponseModelDto<ProductDto>.Fail("Ürün bulunmadı.", HttpStatusCode.NotFound);
            //}

            var productAsDto = mapper.Map<ProductDto>(hasProduct);

            return ResponseModelDto<ProductDto?>.Success(productAsDto);
        }

        public async Task<ResponseModelDto<NoContent>> Update(int productId, ProductUpdateRequestDto request)
        {
            var hasProduct = await productRepository.GetById(productId);

            //if (hasProduct is null)
            //{
            //    return ResponseModelDto<NoContent>.Fail("Güncellemeye çalışılan ürün bulunamadı.", HttpStatusCode.NotFound);
            //}

            hasProduct.Name = request.Name;
            hasProduct.Price = request.Price;

            await productRepository.Update(hasProduct);

            await unitOfWork.CommitAsync();
            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        public async Task<ResponseModelDto<NoContent>> UpdateProductName(int id, string name)
        {
            await productRepository.UpdateProductName(name, id);

            await unitOfWork.CommitAsync();

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        public void TryCatchExample(string price)
        {

            try
            {

            }
            catch (Exception ex)
            {
                throw;
                // throw new Exception(ex.Message);
            }



            if (decimal.TryParse(price, out decimal newPrice))
            {


            }
            else
            {

            }


        }
    }
}
