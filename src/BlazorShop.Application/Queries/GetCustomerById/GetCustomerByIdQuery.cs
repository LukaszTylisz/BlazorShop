using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetCustomerById;

public class GetCustomerByIdQuery(Guid id) : IRequest<CustomerDto>
{
    public Guid Id { get; } = id;
}