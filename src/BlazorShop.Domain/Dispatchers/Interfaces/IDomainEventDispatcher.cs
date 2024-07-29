using BlazorShop.Domain.Core;

namespace BlazorShop.Domain.Dispatchers.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEvents<T>(T aggregate) where T : AggregateRoot;
}