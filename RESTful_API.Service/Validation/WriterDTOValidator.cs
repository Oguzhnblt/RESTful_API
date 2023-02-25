using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation
{
    public class WriterDTOValidator : AbstractValidator<WriterDTO>
    {
        public WriterDTOValidator()
        {
            RuleFor(a => a.WriterFirstName)
              .NotEmpty().WithMessage("Ad alanı boş geçilemez")
              .MaximumLength(50).WithMessage("Ad alanı 50 karakterden fazla olamaz");

            RuleFor(a => a.WriterLastName)
                .NotEmpty().WithMessage("Soyad alanı boş geçilemez")
                .MaximumLength(50).WithMessage("Soyad alanı 50 karakterden fazla olamaz");

            RuleFor(a => a.WriterBirthDate)
                .LessThan(DateTime.Now).WithMessage("Doğum tarihi geçerli bir tarih olmalıdır");
        }
    }
}
