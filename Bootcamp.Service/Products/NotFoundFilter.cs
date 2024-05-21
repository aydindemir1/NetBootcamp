using Bootcamp.Repository.Products;
using Bootcamp.Service.Products.DTOs;
using Bootcamp.Service.SharedDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bootcamp.Service.Products
{
    public class NotFoundFilter(IProductRepository2 productRepository) : Attribute, IActionFilter
    {


        public void OnActionExecuting(ActionExecutingContext context)
        {

            // fast fail
            // guard clauses
            var actionName = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;



            var productIdFromAction = context.ActionArguments.Values.First()!;
            int productId = 0;

            if (actionName == "UpdateProductName" && productIdFromAction is ProductNameUpdateRequestDto productNameUpdateRequestDto)
            {

                productId = productNameUpdateRequestDto.Id;
            }


            if (productId == 0 && !int.TryParse(productIdFromAction.ToString(), out productId))
            {
                return;

            }

            var hasProduct = productRepository.HasExist(productId).Result;

            if (!hasProduct)
            {
                var errorMessage = $"There is no product with id: {productId}";

                var responseModel = ResponseModelDto<NoContent>.Fail(errorMessage);
                context.Result = new NotFoundObjectResult(responseModel);
            }

            //if (actionName == "Delete")
            //{
            //    var productIdFromAction = context.ActionArguments.Values.First()!;

            //    if(int.TryParse(productIdFromAction.ToString(), out int productId))
            //    {
            //        var hasProduct = productRepository.GetById(productId).Result;
            //        if (hasProduct is null)
            //        {
            //            var errorMessage = $"There is no product with id: {productId}";

            //            var responseModel = ResponseModelDto<NoContent>.Fail(errorMessage);
            //            context.Result = new NotFoundObjectResult(responseModel);
            //        }
            //    }


            //}







            // productId => delete
            // productId => update
            // ProductNameUpdateRequestDto => updateProductName
            // productId => GetById
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
