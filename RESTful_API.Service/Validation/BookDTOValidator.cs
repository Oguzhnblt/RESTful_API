using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation
{

    public class BookDTOValidator : AbstractValidator<BookDTO>
    {
        public BookDTOValidator()
        {
            RuleFor(p => p.BookName).NotNull().WithMessage("{PropertyName} alan gereklidir.").NotEmpty().WithMessage("{PropertyName} alan gereklidir.");
            RuleFor(p => p.Title).NotNull().WithMessage("{PropertyName} alan gereklidir.").NotEmpty().WithMessage("{PropertyName} alan gereklidir.");
            RuleFor(p => p.PageCount).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} Fiyat 0'dan küçük  olamaz.");
        }
    }

}
