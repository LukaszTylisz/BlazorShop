using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var orderRm = await mongoDbRepository.GetById<OrderReadModel>(request.Id);
        var orderDto = mapper.Map<OrderDto>(orderRm);

        return orderDto;
    }
}