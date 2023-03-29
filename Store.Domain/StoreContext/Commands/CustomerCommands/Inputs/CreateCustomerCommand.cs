using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.StoreContext.Extensions;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
public class CreateCustomerCommand : Notifiable<Notification>, ICommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public bool Valid()
    {
        AddNotifications(
            new Contract<CreateCustomerCommand>()
            .Requires()
            .IsName(FirstName, 2, 100, "CreateCustomerCommand.FirstName", "Nome inválido")
            .IsName(LastName, 2, 100, "CreateCustomerCommand.LastName", "Sobrenome inválido")
            .IsCPF(Document, "CreateCustomerCommand.Document", "CPF inválido")
            .IsEmail(Email, "CreateCustomerCommand.Email")
            .IsPhone(Phone, "CreateCustomerCommand.Phone", "Número de telefone inválido")
        );
        return IsValid;
    }
}