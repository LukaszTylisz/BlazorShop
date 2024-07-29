using MediatR;

namespace BlazorShop.Application.Commands.CreateProduct;

public class CreateProductCommand(Guid id, string name, decimal price) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public decimal Price { get; } = price;
}