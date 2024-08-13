using BlazorShop.Domain.Aggregates.OrderAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.Street);
        var order = new Order(request.Id, request.CustomerId, address);

        foreach (var item in request.OrderItems)
        {
            order.AddOrderItem(item.ProductId, item.Quantity, item.Price);
        }

        await eventStoreRepository.Save(order);
        await domainEventDispatcher.DispatchEvents(order);
    }
}