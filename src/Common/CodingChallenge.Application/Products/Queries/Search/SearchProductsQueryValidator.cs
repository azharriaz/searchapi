using FluentValidation;

namespace CodingChallenge.Application.Products.Queries.Search
{
    public class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
    {
        public SearchProductsQueryValidator()
        {
            RuleFor(x => x.Query)
                .NotNull()
                .NotEmpty().WithMessage("Query is required.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(50)
                .WithMessage("PageNumber must be at least greater than 0 and less than 50");
        }
    }
}
