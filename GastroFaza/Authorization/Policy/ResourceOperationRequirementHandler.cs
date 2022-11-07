using GastroFaza.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GastroFaza.Authorization.Policy
{

    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Order order)
        {
            if(requirement.ResourcOperation == ResourcOperation.Read || 
               requirement.ResourcOperation == ResourcOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId =  context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (order.AddedById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
