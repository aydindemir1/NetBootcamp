using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.DTOs;
using NetBootcamp.API.Products.DTOs;
using System.Collections.Immutable;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace NetBootcamp.API.Products
{
    public class ProductService(IProductRepository productRepository):IProductService
    {

        //private readonly IProductRepository productRepository;

        //public ProductService(IProductRepository productRepository)
        //{
        //    productRepository = productRepository;
        //}

        public ResponseModelDto<ImmutableList<ProductDto>> GetAllWithCalculatedTax([FromServices] PriceCalculator priceCalculator)
        {

            var productList = productRepository.GetAll().Select(product => new ProductDto(
                 product.Id,
                 product.Name,
                 priceCalculator.CalculateKdv(product.Price, 1.20m),
                 product.Created.ToShortDateString()
                 )).ToImmutableList();

            return ResponseModelDto<ImmutableList<ProductDto>>.Success(productList);
        }





        public ResponseModelDto<ProductDto?> GetByIdWithCalculatedTax(int id, [FromServices] PriceCalculator priceCalculator)
        {
            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<ProductDto>.Fail("Ürün bulunmadı.", HttpStatusCode.NotFound);
            }



            var newDto = new ProductDto(
                 hasProduct.Id,
                 hasProduct.Name,
                 priceCalculator.CalculateKdv(hasProduct.Price, 1.20m),
                 hasProduct.Created.ToShortDateString()
                 );

            return ResponseModelDto<ProductDto?>.Success(newDto);
        }

        public ResponseModelDto<int> Create(ProductCreateRequestDto request)
        {
            var newProduct = new Product
            {
                Id = productRepository.GetAll().Count + 1,
                Name = request.Name,
                Price = request.Price,
                Created = DateTime.Now
            };

            productRepository.Create(newProduct);

            return ResponseModelDto<int>.Success(newProduct.Id, HttpStatusCode.Created);
        }

        public ResponseModelDto<NoContent> Update(int productId, ProductUpdateRequestDto request)
        {
            var hasProduct = productRepository.GetById(productId);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Güncellemeye çalışılan ürün bulunamadı.", HttpStatusCode.NotFound);
            }

            var updatedProduct = new Product
            {
                Id = productId,
                Name = request.Name,
                Price = request.Price,
                Created = hasProduct.Created,
            };

            productRepository.Update(updatedProduct);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }


        public ResponseModelDto<NoContent> Delete(int id)
        {
            var hasProduct = productRepository.GetById(id);

            if (hasProduct is null)
            {
                return ResponseModelDto<NoContent>.Fail("Silinmeye çalışılan ürün bulunamadı.", HttpStatusCode.NotFound);
            }

            productRepository.Delete(id);

            return ResponseModelDto<NoContent>.Success(HttpStatusCode.NoContent);
        }

        

    }
}
