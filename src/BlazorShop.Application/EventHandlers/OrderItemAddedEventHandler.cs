using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.EventHandlers;

public class OrderItemAddedEventHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : INotificationHandler<OrderItemAddedEvent>
{
    public async Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
    {
        var orderRm = await mongoDbRepository.GetById<OrderReadModel>(notification.OrderId);

        var orderItemRm = orderRm.OrderItems.SingleOrDefault(x => x.ProductId == notification.ProductId);
        if (orderItemRm != null)
        {
            orderItemRm.Quantity += notification.Quantity;
        }
        else
        {
            var productRm = await mongoDbRepository.GetById<ProductReadModel>(notification.ProductId);

            orderItemRm = new OrderItemReadModel();
            orderItemRm.ProductId = notification.ProductId;
            orderItemRm.ProductName = productRm.Name;
            orderItemRm.Quantity = notification.Quantity;
            orderItemRm.Price = notification.Price;
            orderRm.OrderItems.Add(orderItemRm);

            orderRm.TotalPrice += orderItemRm.Price * orderItemRm.Quantity;
            orderRm.TotalQuantity += orderItemRm.Quantity;
        }

        await mongoDbRepository.Update<OrderReadModel>(notification.OrderId, orderRm);
    }
}