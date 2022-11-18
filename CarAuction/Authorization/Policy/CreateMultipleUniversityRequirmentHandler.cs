using CarAuction.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CarAuction.Authorization.Policy
{
    public class CreateMultipleUniversityRequirmentHandler : AuthorizationHandler<CreateMultipleUniversityRequirment>
    {

        private readonly AuctionDbContext dbContext;
        public CreateMultipleUniversityRequirmentHandler(AuctionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleUniversityRequirment requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var createdUni = this.dbContext.Vehicles.Count(r => r.CreateById == userId);


            if(createdUni >= requirement.MinimumCreatedUni)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
