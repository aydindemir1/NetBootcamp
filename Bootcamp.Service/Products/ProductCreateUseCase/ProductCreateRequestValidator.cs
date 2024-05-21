using Bootcamp.Repository.Products;
using FluentValidation;

namespace Bootcamp.Service.Products.ProductCreateUseCase
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequestDto>
    {
        public ProductCreateRequestValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")
                .Length(5, 10).WithMessage("{PropertyName} must be between 5 and 10 characters")
                .Must(productName => ExistProductName(productRepository, productName))
                .WithMessage("Product name already exists.");

            RuleFor(x => x.Price).InclusiveBetween(3, 100).WithMessage("Fiyat alanı 3 ile 100 arasında olmalıdır.");

            //RuleFor(x => x.IdentityNo).Length(11).WithMessage("Tc numarası 11 haneli olmalıdır.")
            //    .Must(CheckIdentityNo).WithMessage("Tc numarası hatalıdır.");
        }

        public bool ExistProductName(IProductRepository productRepository, string name)
        {

            return !productRepository.IsExists(name);

            //var hasProduct = productRepository.IsExists(name);

            //return !hasProduct;

            //return hasProduct == true ? true:false ;

            //if (hasProduct)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}

        }
        // Deleagates in .NET
        // 1. Action => void
        // 2. Predicate => bool
        // 3. Func => dynamic
        public static bool CheckIdentityNo(string IdentityNo)
        {
            // business validation
            return true;
        }


    }
}
