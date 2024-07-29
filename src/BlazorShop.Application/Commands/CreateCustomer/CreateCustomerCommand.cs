using MediatR;

namespace BlazorShop.Application.Commands.CreateCustomer;

public class CreateCustomerCommand(Guid id, string name) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}