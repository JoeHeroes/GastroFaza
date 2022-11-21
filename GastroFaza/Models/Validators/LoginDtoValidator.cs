using FluentValidation;
using GastroFaza.Models.DTO;

namespace GastroFaza.Models.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {

        public LoginDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email must be valid."); ;
            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must contain minimum 6 characters.");

        }
    }
}
