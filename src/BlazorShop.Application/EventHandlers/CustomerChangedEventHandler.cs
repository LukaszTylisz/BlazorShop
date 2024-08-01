using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class CustomerChangedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<CustomerChangeEvent>
{
    public async Task Handle(CustomerChangeEvent notification, CancellationToken cancellationToken)
    {
        var customerRm = mapper.Map<CustomerReadModel>(notification);
        await mongoDbRepository.Update<CustomerReadModel>(notification.Id, customerRm);
    }
}