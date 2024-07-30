using FluentValidation;

namespace BlazorShop.Application.Commands.UpdateProduct;

 public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Price)
                .InclusiveBetween(1, 1000);
        }
    }