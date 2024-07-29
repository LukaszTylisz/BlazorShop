using BlazorShop.Domain.Aggregates.ProductAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.CreateProduct;

public class CreateProductCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<CreateProductCommand, Unit>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Id, request.Name, request.Price);
        
        await eventStoreRepository.Save(product);
        await domainEventDispatcher.DispatchEvents(product);
        
        return Unit.Value;
    }
}