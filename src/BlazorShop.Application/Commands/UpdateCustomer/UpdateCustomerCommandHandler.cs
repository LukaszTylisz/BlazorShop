using BlazorShop.Application.Exceptions;
using BlazorShop.Domain.Aggregates.CustomerAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<UpdateCustomerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await eventStoreRepository.Load<Customer>(request.Id);
        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);
        
        customer.Change(request.Name);
        
        await eventStoreRepository.Save(customer);
        await domainEventDispatcher.DispatchEvents(customer);

        return Unit.Value;
    }
}