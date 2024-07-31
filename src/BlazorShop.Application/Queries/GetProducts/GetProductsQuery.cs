using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetProducts;

public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
{
}