using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class ProductCreatedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<ProductCreatedEvent>
{
    public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
    {
        var productRm = mapper.Map<ProductReadModel>(notification);
        await mongoDbRepository.Insert<ProductReadModel>(productRm);
    }
}