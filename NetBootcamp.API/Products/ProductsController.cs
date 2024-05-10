using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetBootcamp.API.Controllers;
using NetBootcamp.API.Products.DTOs;

namespace NetBootcamp.API.Products
{
    public class ProductsController : CustomBaseController
    {
       // private readonly IProductService _productService = ProductServiceFactory.GetService();

        private readonly IProductService _productService;
        public ProductsController(IProductService productService) 
        {
            _productService = productService;
        }
        // baseUrl/api/products
        [HttpGet]
        public IActionResult GetAll(PriceCalculator priceCalculator )
        {

            return Ok(_productService.GetAllWithCalculatedTax(priceCalculator));
        }

        // query string => baseUrl/api/products?id=1
        // root data => baseUrl/api/products/1

        [HttpGet("{productId}")]
        public IActionResult GetById(int productId,  PriceCalculator priceCalculator)
        {

            return CreateActionResult(_productService.GetByIdWithCalculatedTax(productId, priceCalculator));

        }

        // complex type => class, record, struct : request body as json
        // simple type => int, string, decimal : query string by default, route data

        [HttpPost]
        public IActionResult Create(ProductCreateRequestDto request)
        {
            var result = _productService.Create(request);

            return CreateActionResult(result, nameof(GetById), new { productId = result.Data });            //var result = _productService.Create(request);


        }

        // PUT localhost/api/products/10
        [HttpPut("{productId}")]
        public IActionResult Update(int productId, ProductUpdateRequestDto request)
        {
            return CreateActionResult(_productService.Update(productId, request));

        }

        // PUT api/products
        //[HttpPut]
        //public IActionResult Update2(ProductUpdateRequestDto request)
        //{
        //    _productService.Update(request);

        //    return NoContent();
        //}


        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {

            return CreateActionResult(_productService.Delete(productId));


        }
    }
}
