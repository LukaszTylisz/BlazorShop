using BlazorShop.Application.Exceptions;
using BlazorShop.Domain.Aggregates.ProductAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await eventStoreRepository.Load<Product>(request.Id);
        if (product == null)
            throw new NotFoundException(nameof(Product), request.Id);
        
        product.Change(request.Name, request.Price);

        await eventStoreRepository.Save(product);
        await domainEventDispatcher.DispatchEvents(product);

        return Unit.Value;
    }
}