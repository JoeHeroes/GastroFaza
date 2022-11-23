using FluentValidation;
using GastroFaza.Models.DTO;

namespace GastroFaza.Models.Validators
{
    public class EditDtoValidator : AbstractValidator<EditClientDto>
    {

        public EditDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty.");

            RuleFor(x => x.DateOfBirth)
               .NotEmpty().WithMessage("Please choose Date of Birth.");

            RuleFor(x => x.Nationality)
               .NotEmpty().WithMessage("Please choose Nationality.");
        }
    }
}
