using BlazorShop.Domain.Core;

namespace BlazorShop.Domain.Dispatchers;

public interface IDomainEventDispatcher
{
    Task DispatchEvents<T>(T aggregate) where T : AggregateRoot;
}