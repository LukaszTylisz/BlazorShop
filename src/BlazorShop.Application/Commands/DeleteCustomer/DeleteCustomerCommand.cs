using MediatR;

namespace BlazorShop.Application.Commands.DeleteCustomer;
public class DeleteCustomerCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; } = id;
}
