using FluentValidation;
using RESTful_API.DTO.Models;

namespace RESTful_API.Service.Validation
{
    public class GenreDTOValidator : AbstractValidator<GenreDTO>
    {
        public GenreDTOValidator()
        {
            RuleFor(x => x.GenreName)
            .NotEmpty().WithMessage("Genre name cannot be empty.")
            .MaximumLength(50).WithMessage("Genre name cannot exceed 50 characters.");

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive field is required.");

        }
    }
}
