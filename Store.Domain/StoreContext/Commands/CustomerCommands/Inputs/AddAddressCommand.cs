using Flunt.Notifications;
using Store.Domain.StoreContext.Enums;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
public class AddAddressCommand : Notifiable<Notification>, ICommand
{
    public AddAddressCommand(string street, string number, string neighborhood, string city, string state, string country, EAddressType type)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        Type = type;
    }

    public Guid Id { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public EAddressType Type { get; set; }

    public bool Valid()
    {
        return IsValid;
    }
}