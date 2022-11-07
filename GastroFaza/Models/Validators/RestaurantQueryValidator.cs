using FluentValidation;

namespace GastroFaza.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<MenuModel>
    {


        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortColumnNames = { nameof(Dish.Name), nameof(Dish.DishType), nameof(Dish.Price) };
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must in [{string.Join(",", allowedPageSizes)}]");
                }
            });

            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must by in [{string.Join(",", allowedSortColumnNames)}]");
        
        
        }
    }
}
