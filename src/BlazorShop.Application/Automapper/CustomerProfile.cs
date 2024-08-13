using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using Shared.DTOs;

namespace BlazorShop.Application.Automapper;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerReadModel, CustomerDto>();
        CreateMap<CustomerCreatedEvent, CustomerReadModel>();
        CreateMap<CustomerChangeEvent, CustomerReadModel>();
    }

}