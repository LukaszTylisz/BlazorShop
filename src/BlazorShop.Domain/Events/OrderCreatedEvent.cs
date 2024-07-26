using BlazorShop.Domain.Aggregates.OrderAggregate;
using MediatR;

namespace BlazorShop.Domain.Events;

public class OrderCreatedEvent : INotification
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public OrderStatus OrderStatus { get; }
    public Address Address { get; }
    public DateTime CreationDate { get; }

    public OrderCreatedEvent(Guid id, Guid customerId, OrderStatus orderStatus, Address address, DateTime creationDate)
    {
        Id = id;
        CustomerId = customerId;
        OrderStatus = orderStatus;
        Address = address;
        CreationDate = creationDate;
    }
}