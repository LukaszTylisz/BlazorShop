using MediatR;

namespace BlazorShop.Application.Commands.UpdateCustomer;

public class UpdateCustomerCommand(Guid id, string name) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public string Name { get; set; } = name;
}