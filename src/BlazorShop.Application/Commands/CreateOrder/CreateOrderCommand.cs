using MediatR;

namespace BlazorShop.Application.Commands.CreateOrder;

public class CreateOrderCommand(
    Guid id,
    Guid customerId,
    string city,
    string street,
    List<CreateOrderItemCommand> orderItems)
    : IRequest
{
    public Guid Id { get; } = id;
    public Guid CustomerId { get; } = customerId;
    public string City { get; } = city;
    public string Street { get; } = street;
    public List<CreateOrderItemCommand> OrderItems { get; } = orderItems;
}

public class CreateOrderItemCommand(Guid productId, int quantity, decimal price)
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;
}