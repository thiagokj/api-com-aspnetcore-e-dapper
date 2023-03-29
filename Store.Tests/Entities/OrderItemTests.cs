using Store.Domain.StoreContext.Entities;

namespace Store.Tests.Entities;
[TestClass]
public class OrderItemTests
{
    private Product _product1;
    private Product _product2;
    private OrderItem _orderItem;
    public OrderItemTests()
    {
        _product1 = new(
        "Mouse",
        "Mouse Gamer 4You",
        "https://url-da-imagem",
        10,
        1
        );

        _product2 = new(
            "Teclado",
            "Teclado Gamer 4You",
            "https://url-da-imagem",
            18,
            22
        );
    }

    [TestMethod]
    public void ShouldReturnErrorWhenQuantityIsGreaterThanQuantityOnHand()
    {
        _orderItem = new(_product1, 5);

        Assert.IsFalse(_orderItem.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenQuantityIsLowerThanQuantityOnHand()
    {
        _orderItem = new(_product1, 1);

        Assert.IsTrue(_orderItem.IsValid);
    }
}