using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetProductById;

public class GetProductByIdQuery(Guid id) : IRequest<ProductDto>
{
    public Guid Id { get; } = id;
}