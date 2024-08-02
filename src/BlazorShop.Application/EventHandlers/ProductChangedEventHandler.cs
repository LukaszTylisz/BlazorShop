using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class ProductChangedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<ProductChangedEvent>
{
    public async Task Handle(ProductChangedEvent notification, CancellationToken cancellationToken)
    {
        var productRm = mapper.Map<ProductReadModel>(notification);
        await mongoDbRepository.Update<ProductReadModel>(notification.Id, productRm);
    }
}