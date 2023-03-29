using Flunt.Notifications;
using Store.Domain.StoreContext.Contracts;
using Store.Domain.StoreContext.ValueObjects;
using Store.Shared.Entities;

namespace Store.Domain.StoreContext.Entities;
public class Customer : Entity
{
    private readonly IList<Address> _addresses = new List<Address>();

    public Customer(
        Name name,
        Document document,
        Email email,
        Phone phone)
    {
        Name = name;
        Document = document;
        Email = email;
        Phone = phone;

        AddNotifications(new CustomerContract(this));
    }
    public Customer(
        Guid id,
        Name name,
        Document document,
        Email email,
        Phone phone)
        : base(id)
    {
        Name = name;
        Document = document;
        Email = email;
        Phone = phone;

        AddNotifications(new CustomerContract(this));
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.ToArray();

    public void AddAddress(Address address)
    {
        _addresses.Add(address);
        AddNotifications(new AddressContract(address));
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}