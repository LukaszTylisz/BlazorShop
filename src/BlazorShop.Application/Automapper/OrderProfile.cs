using AutoMapper;
using BlazorShop.Application.ReadModels;
using Shared.DTOs;

namespace BlazorShop.Application.Automapper;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderReadModel, OrderDto>();
        CreateMap<OrderItemReadModel, OrderItemDto>();
    }
}
