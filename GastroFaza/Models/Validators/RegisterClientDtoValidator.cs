using FluentValidation;
using GastroFaza.Models.DTO;

namespace GastroFaza.Models.Validators
{
    public class RegisterClientDtoValidator : AbstractValidator<RegisterClientDto>
    {

        public RegisterClientDtoValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) => 
                {
                    var emailInUse = dbContext.Clients.Any(u => u.Email ==value);
                    if (emailInUse)
                    {
                        context.AddFailure("Emial", "That email is taken");
                    }
                });
        }
    }
}
