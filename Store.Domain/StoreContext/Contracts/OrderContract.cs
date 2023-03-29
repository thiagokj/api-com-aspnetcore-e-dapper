using Flunt.Validations;
using Store.Domain.StoreContext.Entities;

namespace Store.Domain.StoreContext.Contracts;
public class OrderContract : Contract<Order>
{
    public OrderContract(Order order)
    {
        Requires()
            .IsGreaterThan(
            order.Items.Count,
            0,
            "OrderContract.order",
            "Não há itens no Pedido");
    }
}