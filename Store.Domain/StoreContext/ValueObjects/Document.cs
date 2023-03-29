using Flunt.Validations;
using Store.Domain.StoreContext.Extensions;

namespace Store.Domain.StoreContext.ValueObjects;
public class Document : ValueObject
{
    public Document(string number)
    {
        Number = number;

        AddNotifications(
            new Contract<Document>()
            .Requires()
            .IsCPF(number, "Document.Number", "CPF inválido")
        );
    }

    public string Number { get; private set; }

    public override string ToString()
    {
        return Number;
    }
}