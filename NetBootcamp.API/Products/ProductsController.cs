﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.Products.DTOs;

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

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestDto request)
        {
            var result = await  _productService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { productId = result.Data });            //var result = _productService.Create(request);


        }

        [HttpPut("UpdateProductName")]
        public async Task<IActionResult> UpdateProductName(ProductNameUpdateRequestDto request)
        {
            return CreateActionResult(await _productService.UpdateProductName(request.Id, request.Name));
        }

        // PUT localhost/api/products/10
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


        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> Delete(int productId)
        {

            return CreateActionResult(await _productService.Delete(productId));


        }
    }
}
