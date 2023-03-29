namespace Store.Domain.StoreContext.Commands.OrderItemCommands.Inputs;
public class OrderItemCommand
{
    public Guid Product { get; set; }
    public decimal Quantity { get; set; }
}