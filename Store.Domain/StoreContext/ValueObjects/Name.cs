using Flunt.Validations;

namespace Store.Domain.StoreContext.ValueObjects;
public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(
            new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(firstName, "Name.firstName")
            .IsBetween(
                firstName.Length,
                2,
                100,
                "Name.firstName",
                "O nome deve estar entre 2 e 100 caracteres")
            .IsNotNullOrEmpty(lastName, "Name.lastName")
            .IsBetween(
                lastName.Length,
                2,
                100,
                "Name.lastName",
                "O sobrenome deve estar entre 2 e 100 caracteres")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}