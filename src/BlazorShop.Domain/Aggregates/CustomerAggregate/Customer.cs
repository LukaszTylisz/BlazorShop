using BlazorShop.Domain.Core;
using BlazorShop.Domain.Events;
using MediatR;

namespace BlazorShop.Domain.Aggregates.CustomerAggregate;

public class Customer : AggregateRoot
{
    public string Name { get; private set; } = default!;

    public Customer() { }

    public Customer(Guid id, string name)
    {
        ApplyChange(new CustomerCreatedEvent(id, name));
    }

    public void Change(string name)
    {
        ApplyChange(new CustomerChangeEvent(Id, name));
    }
    
    protected override void When(INotification @event)
    {
        switch (@event)
        {
            case CustomerCreatedEvent e:
                Id = e.Id;
                Name = e.Name;
                break;
            case CustomerChangeEvent e:
                Name = e.Name;
                break;
        }
    }
}