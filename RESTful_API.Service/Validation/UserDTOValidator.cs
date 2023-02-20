using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {

            RuleFor(p => p.Username).NotNull().WithMessage("{PropertyName} alan gereklidir.").NotEmpty().WithMessage("{PropertyName} alan gereklidir.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre gereklidir.")
                .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir sayı içermelidir.");


        }
    }
}
