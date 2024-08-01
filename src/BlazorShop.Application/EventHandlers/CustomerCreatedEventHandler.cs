using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class CustomerCreatedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<CustomerCreatedEvent>
{
    public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        var customerRm = mapper.Map<CustomerReadModel>(notification);
        await mongoDbRepository.Insert<CustomerReadModel>(customerRm);
    }
}