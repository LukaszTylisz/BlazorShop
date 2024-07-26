using MediatR;

namespace BlazorShop.Domain.Events;

public class ProductDeletedEvent(Guid id) : INotification
{
    public Guid Id { get; } = id;
}