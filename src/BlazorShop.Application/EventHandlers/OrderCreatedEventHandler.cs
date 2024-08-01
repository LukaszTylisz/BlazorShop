using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.Enums;

namespace BlazorShop.Application.EventHandlers;

public class OrderCreatedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        var customerRm = await mongoDbRepository.GetById<CustomerReadModel>(notification.CustomerId);

        var orderRm = new OrderReadModel
        {
            Id = notification.Id,
            CustomerId = notification.CustomerId,
            CustomerName = customerRm.Name,
            City = notification.Address.City,
            Street = notification.Address.Street,
            OrderStatus = (OrderStatus)notification.OrderStatus,
            CreationDate = notification.CreationDate,
            OrderItems = []
        };

        await mongoDbRepository.Insert<OrderReadModel>(orderRm);
    }
}