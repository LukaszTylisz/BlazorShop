using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetCustomers;

public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
{
}