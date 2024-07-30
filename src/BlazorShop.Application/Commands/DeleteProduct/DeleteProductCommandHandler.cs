using BlazorShop.Application.Exceptions;
using BlazorShop.Domain.Aggregates.ProductAggregate;
using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;

namespace BlazorShop.Application.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IDomainEventDispatcher domainEventDispatcher,
    IEventStoreRepository eventStoreRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await eventStoreRepository.Load<Product>(request.Id);
        if (product == null)
            throw new NotFoundException(nameof(Product), request.Id);
        
        product.Delete();
        
        await eventStoreRepository.Save(product);
        await domainEventDispatcher.DispatchEvents(product);
        
        return Unit.Value;
    }
}