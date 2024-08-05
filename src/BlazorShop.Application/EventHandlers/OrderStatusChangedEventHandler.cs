using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.Enums;

namespace BlazorShop.Application.EventHandlers;

public class OrderStatusChangedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<OrderStatusChangedEvent>
{
    public async Task Handle(OrderStatusChangedEvent notification, CancellationToken cancellationToken)
    {
        var orderRm = await mongoDbRepository.GetById<OrderReadModel>(notification.Id);
        orderRm.OrderStatus = (OrderStatus)notification.OrderStatus;

        await mongoDbRepository.Update<OrderReadModel>(notification.Id, orderRm);
    }
}