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

        var orderRm = new OrderReadModel();
        orderRm.Id = notification.Id;
        orderRm.CustomerId = notification.CustomerId;
        orderRm.CustomerName = customerRm.Name;
        orderRm.City = notification.Address.City;
        orderRm.Street = notification.Address.Street;
        orderRm.OrderStatus = (OrderStatus)notification.OrderStatus;
        orderRm.CreationDate = notification.CreationDate;
        orderRm.OrderItems = new List<OrderItemReadModel>();

        await mongoDbRepository.Insert<OrderReadModel>(orderRm);
    }
}