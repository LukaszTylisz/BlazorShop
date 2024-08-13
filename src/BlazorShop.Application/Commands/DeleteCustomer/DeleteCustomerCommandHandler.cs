using BlazorShop.Application.Exceptions;
using BlazorShop.Domain.Aggregates.CustomerAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.DeleteCustomer;
public class DeleteCustomerCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<DeleteCustomerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await eventStoreRepository.Load<Customer>(request.Id);

        if (customer == null)
            throw new NotFoundException(nameof(Customer), request.Id);
        customer.Delete();

        await eventStoreRepository.Save(customer);
        await domainEventDispatcher.DispatchEvents(customer);

        return Unit.Value;
    }
}
