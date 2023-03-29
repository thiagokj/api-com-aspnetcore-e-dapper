using Flunt.Validations;
using Store.Domain.StoreContext.Extensions;

namespace Store.Domain.StoreContext.ValueObjects;
public class Phone : ValueObject
{
    public Phone(string number)
    {
        Number = number;

        AddNotifications(
            new Contract<Phone>()
            .Requires()
            .IsPhone(number, "Phone.number", "O número deve possuir 11 dígitos")
        );
    }

    public string Number { get; private set; }

    public override string ToString()
    {
        return Number;
    }
}