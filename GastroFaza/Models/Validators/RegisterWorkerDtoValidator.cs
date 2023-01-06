using FluentValidation;
using GastroFaza.Models.DTO;

namespace GastroFaza.Models.Validators
{
    public class RegisterWorkerDtoValidator : AbstractValidator<RegisterWorkerDto>
    {
        public RegisterWorkerDtoValidator(RestaurantDbContext dbContext)
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
                    var emailInUse = dbContext.Workers.Any(u => u.Email ==value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
