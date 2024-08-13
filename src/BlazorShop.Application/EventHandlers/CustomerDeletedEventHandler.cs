using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;
public class CustomerDeletedEventHandlerI(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<CustomerDeletedEvent>
{
    public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        await mongoDbRepository.Delete<CustomerReadModel>(notification.Id);
    }
}
