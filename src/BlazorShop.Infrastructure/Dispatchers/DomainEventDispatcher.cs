using BlazorShop.Domain.Core;
using BlazorShop.Domain.Dispatchers.Interfaces;
using MediatR;

namespace BlazorShop.Infrastructure.Dispatchers;

public class DomainEventDispatcher(IPublisher mediator) : IDomainEventDispatcher
{
    public async Task DispatchEvents<T>(T aggregate) where T : AggregateRoot
    {
        foreach (var domainEvent in aggregate.GetDomainEvents())
        {
            await mediator.Publish(domainEvent);
        }
        
        aggregate.ClearDomainEvents();
    }
}