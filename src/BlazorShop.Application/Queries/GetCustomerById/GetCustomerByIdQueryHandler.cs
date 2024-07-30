using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Repositories.Interfaces;
using MediatR;
using Shared.DTOs;

namespace BlazorShop.Application.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(IMongoDbRepository mongoDbRepository, IMapper mapper)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customerRm = await mongoDbRepository.GetById<CustomerReadModel>(request.Id);
        var customerDto = mapper.Map<CustomerDto>(customerRm);

        return customerDto;
    }
}