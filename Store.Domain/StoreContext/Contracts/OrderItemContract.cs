using Flunt.Validations;
using Store.Domain.StoreContext.Entities;

namespace Store.Domain.StoreContext.Contracts;
public class OrderItemContract : Contract<OrderItem>
{
    public OrderItemContract(OrderItem orderItem)
    {
        Requires()
            .IsLowerOrEqualsThan(
            orderItem.Quantity,
            orderItem.Product.QuantityOnHand,
            "OrderItemContract.orderItem",
            $"A quantidade de {orderItem.Product.ToString().ToUpper()} solicitada é maior que a quantidade do produto em estoque");
    }
}