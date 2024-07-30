using AutoMapper;
using BlazorShop.Application.Queries.GetCustomerById;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetCustomers;

public class GetCustomersQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customerRm = await mongoDbRepository.GetAll<CustomerReadModel>();
        var customersDto = mapper.Map<IEnumerable<CustomerDto>>(customerRm);

        return customersDto;
    }
}