using BlazorShop.Domain.Core;

namespace BlazorShop.Domain.Aggregates.OrderAggregate;

public class Address : ValueObject
{
    public string City { get; private set; }
    public string Street { get; private set; }

    public Address(string city, string street)
    {
        City = city;
        Street = street;
    }
}