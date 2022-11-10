using CarAuction.Entites;
using FluentValidation;

namespace CarAuction.Models.Validators
{
    public class AuctionQueryValidator : AbstractValidator<AuctionQuery>
    {


        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortColumnNames = { nameof(Vehicle.modelGeneration), nameof(Vehicle.modelSpecifer), nameof(Vehicle.registrationYear) };
        public AuctionQueryValidator()
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
