using Flunt.Notifications;
using Flunt.Validations;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Commands;
public class DeleteCommand : Notifiable<Notification>, ICommand
{
    public DeleteCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }

    public bool Valid()
    {
        AddNotifications(
                   new Contract<DeleteCommand>()
                   .Requires()
                   .IsNotNullOrEmpty(
                       Id,
                       "DeleteCommand.Id",
                       "O Id não pode ser nulo ou vazio")
                   .AreEquals(
                       Id.Length,
                       36,
                       "DeleteCommand.Id",
                       "o Guid não corresponde a 36 dígitos")
               );
        return IsValid;
    }
}