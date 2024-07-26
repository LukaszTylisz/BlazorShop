using MediatR;

namespace BlazorShop.Domain.Events;

public class CustomerChangeEvent(Guid id, string name) : INotification
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}