using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetOrders;

public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
}