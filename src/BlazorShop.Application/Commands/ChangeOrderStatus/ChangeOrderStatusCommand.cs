using MediatR;
using Shared.Enums;

namespace BlazorShop.Application.Commands.ChangeOrderStatus;

public class ChangeOrderStatusCommand(Guid id, OrderStatus orderStatus) : IRequest<Unit>
{
    public Guid Id { get; } = id;
    public OrderStatus OrderStatus { get; } = orderStatus;
}