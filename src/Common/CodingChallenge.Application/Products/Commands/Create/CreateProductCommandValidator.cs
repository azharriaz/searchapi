using FluentValidation;

namespace CodingChallenge.Application.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .NotEmpty().WithMessage("Name is required.");
           
            RuleFor(v => v.Description)
                .MaximumLength(400).WithMessage("Description must not exceed 400 characters.")
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(v => v.Price)
                .NotEmpty().WithMessage("Price is required.");

            RuleFor(v => v.BrandId)
                .NotEmpty().WithMessage("Product Brand Id is required.");

            RuleFor(v => v.TypeId)
                .NotEmpty().WithMessage("Product Type Id is required.");
        }
    }
}
