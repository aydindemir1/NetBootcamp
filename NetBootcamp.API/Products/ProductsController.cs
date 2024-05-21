﻿using Bootcamp.Service.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.Products.Helpers;
using Bootcamp.Service.Products.ProductCreateUseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.Filters;

namespace NetBootcamp.API.Products
{
    public class ProductsController : CustomBaseController
    {
       // private readonly IProductService _productService = ProductServiceFactory.GetService();

        private readonly IProductService2 _productService;
        public ProductsController(IProductService2 productService) 
        {
            _productService = productService;
        }
        // baseUrl/api/products
        [HttpGet]
        public async Task<IActionResult> GetAll(PriceCalculator priceCalculator )
        {

            return Ok( await _productService.GetAllWithCalculatedTax(priceCalculator));
        }

        // query string => baseUrl/api/products?id=1
        // root data => baseUrl/api/products/1
        [ServiceFilter(typeof(NotFoundFilter))]
        [MyResourceFilter]
        [MyActionFilter]
        [MyResultFilter]
        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetById(int productId,  PriceCalculator priceCalculator)
        {

            return CreateActionResult(await _productService.GetByIdWithCalculatedTax(productId, priceCalculator));

        }

        [HttpGet("page/{page:int}/pagesize/{pageSize:max(50)}")]
        public async Task<IActionResult> GetAllByPage(int page, int pageSize, [FromServices] PriceCalculator priceCalculator)
        {
            return CreateActionResult(await _productService.GetAllByPageWithCalculatedTax(priceCalculator, page, pageSize));
        }

        // complex type => class, record, struct : request body as json
        // simple type => int, string, decimal : query string by default, route data

        [SendSmsWhenExceptionFilter]
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestDto request)
        {
           // throw new Exception("db'ye gidemedi");
            var result = await  _productService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { productId = result.Data });            //var result = _productService.Create(request);


        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPut("UpdateProductName")]
        public async Task<IActionResult> UpdateProductName(ProductNameUpdateRequestDto request)
        {
            return CreateActionResult(await _productService.UpdateProductName(request.Id, request.Name));
        }

        // PUT localhost/api/products/10
        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpPut("{productId:int}")]
        public async Task<IActionResult> Update(int productId, ProductUpdateRequestDto request)
        {
            return CreateActionResult(await _productService.Update(productId, request));

        }
         
        // PUT api/products
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {

            return CreateActionResult(await _productService.Delete(productId));


        }
    }
}
