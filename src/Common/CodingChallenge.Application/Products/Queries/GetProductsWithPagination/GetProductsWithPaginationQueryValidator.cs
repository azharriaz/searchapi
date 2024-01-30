using FluentValidation;

namespace CodingChallenge.Application.Products.Queries.GetProductsWithPagination
{
    public class SearchProductsQueryValidator : AbstractValidator<GetProductsWithPaginationQuery>
    {
        public SearchProductsQueryValidator()
        {
            RuleFor(x=>x.ProductId)
                .NotNull()
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
