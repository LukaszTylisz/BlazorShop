using AutoMapper;
using BlazorShop.Application.Queries.GetOrders;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Aggregates.ProductAggregate;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetProducts;

public class GetProductsQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productRm = await mongoDbRepository.GetAll<ProductReadModel>();
        var productDto = mapper.Map<IEnumerable<ProductDto>>(productRm);

        return productDto;
    }
}