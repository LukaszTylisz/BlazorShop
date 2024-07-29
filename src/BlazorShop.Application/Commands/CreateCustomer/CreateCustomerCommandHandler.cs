using BlazorShop.Domain.Aggregates.CustomerAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.CreateCustomer;

public class CreateCustomerCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateCustomerCommand, Unit>
{
    public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Id, request.Name);
        
        await eventStoreRepository.Save(customer);
        await domainEventDispatcher.DispatchEvents(customer);
        
        return Unit.Value;
    }
}