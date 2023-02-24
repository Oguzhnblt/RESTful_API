using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation.BookValidator
{
    public class BookDTOGetByIdValidator : AbstractValidator<BookDTO>
    {
        public BookDTOGetByIdValidator()
        {
            RuleFor(p => p.ID).NotEmpty().GreaterThan(0).WithMessage("{Property} boş olamaz ve 0 dan büyük olmalıdır.");
        }
    }
}
