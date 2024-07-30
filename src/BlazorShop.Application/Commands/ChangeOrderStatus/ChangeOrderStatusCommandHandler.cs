using BlazorShop.Application.Exceptions;
using BlazorShop.Domain.Aggregates.OrderAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.ChangeOrderStatus;

public class ChangeOrderStatusCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<ChangeOrderStatusCommand, Unit>
{
    public async Task<Unit> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await eventStoreRepository.Load<Order>(request.Id);
        
        if(order == null)
            throw new NotFoundException(nameof(Order), request.Id);

        switch ((OrderStatus)request.OrderStatus)
        {
            case OrderStatus.New:
                order.SetNewStatus();
                break;
            case OrderStatus.Paid:
                order.SetPaidStatus();
                break;
            case OrderStatus.Shipped:
                order.SetShippedStatus();
                break;
            case OrderStatus.Cancelled:
                order.SetCancelledStatus();
                break;
        }
        
        await eventStoreRepository.Save(order);
        await domainEventDispatcher.DispatchEvents(order);
        
        return Unit.Value;
    }
}