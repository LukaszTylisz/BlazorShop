using Shared.Enums;

namespace Shared.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = default!;

    public string City { get; set; } = default!;

    public string Street { get; set; } = default!;

    public OrderStatus OrderStatus { get; set; }

    public DateTime CreationDate { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = default!;

    public decimal TotalPrice { get; set; }

    public int TotalQuantity { get; set; }
}

public class OrderItemDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = default!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}