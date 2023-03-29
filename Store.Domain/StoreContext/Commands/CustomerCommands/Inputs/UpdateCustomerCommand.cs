using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.StoreContext.Extensions;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Commands.CustomerCommands.Inputs;
public class UpdateCustomerCommand : Notifiable<Notification>, ICommand
{
    public UpdateCustomerCommand(Guid id, string firstName, string lastName, string document, string email, string phone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Document = document;
        Email = email;
        Phone = phone;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public bool Valid()
    {
        AddNotifications(
            new Contract<UpdateCustomerCommand>()
            .Requires()
            .IsName(FirstName, 2, 100, "UpdateCustomerCommand.FirstName", "Nome inválido")
            .IsName(LastName, 2, 100, "UpdateCustomerCommand.LastName", "Sobrenome inválido")
            .IsCPF(Document, "UpdateCustomerCommand.Document", "CPF inválido")
            .IsEmail(Email, "UpdateCustomerCommand.Email")
            .IsPhone(Phone, "UpdateCustomerCommand.Phone", "Número de telefone inválido")
        );
        return IsValid;
    }
}