using MediatR;

namespace BlazorShop.Domain.Events;
public class CustomerDeletedEvent(Guid id) : INotification
{
    public Guid Id { get; } = id;
}