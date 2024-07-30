using MediatR;

namespace BlazorShop.Application.Commands.DeleteProduct;

public class DeleteProductCommand(Guid id) : IRequest<Unit>
{
    public Guid Id { get; } = id;
}