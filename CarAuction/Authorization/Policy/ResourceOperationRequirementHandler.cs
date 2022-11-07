using CarAuction.Entites;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarAuction.Authorization.Policy
{

    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Vehicle>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Vehicle vehicle)
        {
            if(requirement.ResourcOperation == ResourcOperation.Read || 
               requirement.ResourcOperation == ResourcOperation.Create)
            {
                context.Succeed(requirement);
            }

            var userId =  context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (vehicle.CreateById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
