using BlazorShop.Domain.Core;

namespace BlazorShop.Domain.Repositories.Interfaces;

public interface IEventStoreRepository
{
    Task<T> Load<T>(Guid aggregateId) where T : AggregateRoot, new();
    Task Save<T>(T aggregate) where T : AggregateRoot, new();
}