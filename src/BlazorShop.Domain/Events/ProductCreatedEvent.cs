using MediatR;

namespace BlazorShop.Domain.Events;

public class ProductCreatedEvent(Guid id, string name, decimal price) : INotification
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public decimal Price { get; } = price;
}