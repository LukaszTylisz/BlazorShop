using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class ProductDeletedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<ProductDeletedEvent>
{
    public async Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        await mongoDbRepository.Delete<ProductReadModel>(notification.Id);
    }
}