using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetOrders;

public class GetOrdersQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orderRm = await mongoDbRepository.GetAll<OrderReadModel>();
        var ordersDto = mapper.Map<IEnumerable<OrderDto>>(orderRm);

        return ordersDto;
    }
}