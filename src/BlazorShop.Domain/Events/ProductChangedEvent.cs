using MediatR;

namespace BlazorShop.Domain.Events;

public class ProductChangedEvent(Guid id, string name, decimal price) : INotification
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public decimal Price { get; } = price;
}