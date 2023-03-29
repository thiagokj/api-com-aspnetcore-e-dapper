using Store.Domain.StoreContext.Contracts;
using Store.Shared.Entities;

namespace Store.Domain.StoreContext.Entities;
public class OrderItem : Entity
{
    public OrderItem(Product product, decimal quantity)
    {
        Product = product;
        Quantity = quantity;
        Price = Product.Price;

        AddNotifications(new OrderItemContract(this));
    }

    public Product Product { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }

    public override string ToString()
    {
        return Product.Title;
    }
}