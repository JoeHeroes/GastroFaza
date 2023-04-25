using FluentValidation;
using GastroFaza.Models.DTO;

namespace GastroFaza.Models.Validators
{
    public class RegisterClientDtoValidator : AbstractValidator<RegisterClientDto>
    {

        public RegisterClientDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(x=>x.Email)
                .EmailAddress().WithMessage("Email must be valid."); ;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name cannot be empty.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name cannot be empty.");

            RuleFor(x => x.Password)
                .MinimumLength(6).WithMessage("Password must contain minimum 6 characters.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password).WithMessage("Passwords must be equal."); ;

            RuleFor(x => x.Email)
                .Custom((value, context) => 
                {
                    var emailInUse = dbContext.Clients.Any(u => u.Email ==value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken.");
                    }
                });

            RuleFor(x => x.DateOfBirth)
               .NotEmpty().WithMessage("Please choose Date of Birth.");
            
            RuleFor(x => x.Nationality)
               .NotEmpty().WithMessage("Please choose Nationality.");

            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("Phone number cannot be empty.");
        }
    }
}
