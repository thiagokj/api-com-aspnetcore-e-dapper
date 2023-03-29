using Flunt.Validations;

namespace Store.Domain.StoreContext.ValueObjects;
public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;

        AddNotifications(
            new Contract<Email>()
            .Requires()
            .IsEmail(address, "Email.address", "Email inválido")
        );
    }

    public string Address { get; private set; }

    public override string ToString()
    {
        return Address;
    }

}