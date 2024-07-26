using BlazorShop.Domain.Core;

namespace BlazorShop.Domain.Aggregates.OrderAggregate;

public class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }

    public OrderItem() { }

    public OrderItem(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }
}