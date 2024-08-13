using AutoMapper;
using BlazorShop.Application.ReadModels;
using BlazorShop.Domain.Events;
using Shared.DTOs;

namespace BlazorShop.Application.Automapper;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductReadModel, ProductDto>();
        CreateMap<ProductCreatedEvent, ProductReadModel>();
        CreateMap<ProductChangedEvent, ProductReadModel>();
    }
}
