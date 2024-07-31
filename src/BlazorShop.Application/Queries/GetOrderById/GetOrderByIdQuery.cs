using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetOrderById;

public class GetOrderByIdQuery(Guid id) : IRequest<OrderDto>
{
    public Guid Id { get; } = id;
}