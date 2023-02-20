using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("{PropertyName} alan gereklidir.").NotEmpty().WithMessage("{PropertyName} alan gereklidir.");
            RuleFor(p => p.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} Fiyat 0'dan küçük  olamaz.");
        }

        
    }
}
