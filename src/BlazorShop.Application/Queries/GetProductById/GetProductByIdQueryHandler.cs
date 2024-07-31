using AutoMapper;
using BlazorShop.Application.Queries.GetOrders;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetProductById;

public class GetProductByIdQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var productRm = await mongoDbRepository.GetById<ProductReadModel>(request.Id);
        var productDto = mapper.Map<ProductDto>(productRm);

        return productDto;
    }
}