using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.StoreContext.Commands.OrderItemCommands.Inputs;
using Store.Shared.Commands;

namespace Store.Domain.StoreContext.Commands.OrderCommands.Inputs;
public class PlaceOrderCommand : Notifiable<Notification>, ICommand
{
    public Guid Customer { get; set; }
    public IList<OrderItemCommand> OrderItems { get; set; }

    public PlaceOrderCommand()
    {
        OrderItems = new List<OrderItemCommand>();
    }

    public bool Valid()
    {
        AddNotifications(
            new Contract<PlaceOrderCommand>()
            .Requires()
            .IsGreaterOrEqualsThan(
                Customer.ToString(),
                36,
                "PlaceOrderCommand.Customer",
                "Id do Cliente inválido")
            .IsGreaterThan(
                OrderItems.Count,
                0,
                "PlaceOrderCommand.OrderItems",
                "A quantidade de itens no pedido deve ser maior que zero")
        );
        return Valid();
    }
}