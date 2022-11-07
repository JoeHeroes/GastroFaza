using GastroFaza.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GastroFaza.Authorization.Policy
{
    public class CreateMultipleRestaurantRequirmentHandler : AuthorizationHandler<CreateMultipleRestaurantRequirment>
    {

        private readonly RestaurantDbContext dbContext; 
        public CreateMultipleRestaurantRequirmentHandler(RestaurantDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantRequirment requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdUni = this.dbContext.Orders.Count(r => r.AddedById == userId);


            if(createdUni >= requirement.MinimumCreatedUni)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
