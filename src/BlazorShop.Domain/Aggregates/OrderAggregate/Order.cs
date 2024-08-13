using BlazorShop.Domain.Core;
using BlazorShop.Domain.Events;
using BlazorShop.Domain.Exceptions;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorShop.Domain.Aggregates.OrderAggregate;

public class Order : AggregateRoot
{
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreationDate { get; private set; }
    public List<OrderItem> OrderItems { get; set; }

    [BsonElement("totalPrice")]
    public decimal TotalPrice { get; private set; } = 0;
    [BsonElement("totalQuantity")]
    public int TotalQuantity { get; private set; } = 0;

    public Order()
    {
    }

    public Order(Guid id, Guid customerId, Address address)
    {
        ApplyChange(new OrderCreatedEvent(id, customerId, OrderStatus.New, address, DateTime.Now));
    }

    public void AddOrderItem(Guid productId, int quantity, decimal price)
    {
        ApplyChange(new OrderItemAddedEvent(Id, productId, quantity, price));
    }

    public void SetNewStatus()
    {
        throw new DomainException(
            $"It's not possible to change the order status from {OrderStatus} to {OrderStatus.New}.");
    }

    public void SetPaidStatus()
    {
        if (OrderStatus != OrderStatus.New)
        {
            throw new DomainException(
                $"It's not possible to change the order status from {OrderStatus} to {OrderStatus.Paid}.");
        }

        ApplyChange(new OrderStatusChangedEvent(Id, OrderStatus.Paid));
    }

    public void SetShippedStatus()
    {
        if (OrderStatus != OrderStatus.Paid)
        {
            throw new DomainException(
                $"It's not possible to change the order status from {OrderStatus} to {OrderStatus.Shipped}.");
        }

        ApplyChange(new OrderStatusChangedEvent(Id, OrderStatus.Shipped));
    }

    public void SetCancelledStatus()
    {
        if (OrderStatus == OrderStatus.Paid || OrderStatus == OrderStatus.Shipped)
        {
            throw new DomainException(
                $"It's not possible to change the order status from {OrderStatus} to {OrderStatus.Cancelled}.");
        }

        ApplyChange(new OrderStatusChangedEvent(Id, OrderStatus.Cancelled));
    }

    protected override void When(INotification @event)
    {
        switch (@event)
        {
            case OrderCreatedEvent e:
                Handle(e);
                break;
            case OrderItemAddedEvent e:
                Handle(e);
                break;
            case OrderStatusChangedEvent e:
                Handle(e);
                break;
        }
    }

    private void Handle(OrderCreatedEvent @event)
    {
        Id = @event.Id;
        CustomerId = @event.CustomerId;
        OrderStatus = @event.OrderStatus;
        Address = @event.Address;
        CreationDate = @event.CreationDate;
        OrderItems = new List<OrderItem>();
        TotalPrice = 0;
        TotalQuantity = 0;
    }

    private void Handle(OrderItemAddedEvent @event)
    {
        var orderItem = OrderItems.SingleOrDefault(x => x.ProductId == @event.ProductId);
        if (orderItem != null)
        {
            orderItem.AddQuantity(@event.Quantity);
        }
        else
        {
            OrderItems.Add(new OrderItem(@event.ProductId, @event.Quantity, @event.Price));
        }

        TotalPrice += @event.Price * @event.Quantity;
        TotalQuantity += @event.Quantity;

        Console.WriteLine($"Handle event - TotalPrice: {TotalPrice}, TotalQuantity: {TotalQuantity}");
    }

    private void Handle(OrderStatusChangedEvent @event)
    {
        OrderStatus = @event.OrderStatus;
    }
}