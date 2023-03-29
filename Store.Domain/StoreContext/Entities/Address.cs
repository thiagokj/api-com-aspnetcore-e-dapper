using Flunt.Notifications;
using Store.Domain.StoreContext.Contracts;
using Store.Domain.StoreContext.Enums;
using Store.Shared.Entities;

namespace Store.Domain.StoreContext.Entities;
public class Address : Entity
{
    public Address(
        string street,
        string number,
        string neighborhood,
        string complement,
        string city,
        string state,
        string country,
        string zipCode,
        EAddressType type)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
        Type = type;

        AddNotifications(new AddressContract(this));
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public EAddressType Type { get; private set; }

    public override string ToString()
    {
        return $"{Street}, {Number} - {City}/{State}";
    }
}