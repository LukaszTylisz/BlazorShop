using FluentValidation;

namespace BlazorShop.Application.Commands.DeleteCustomer;
public class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
