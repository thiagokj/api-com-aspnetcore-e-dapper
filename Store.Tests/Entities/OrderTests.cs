using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Enums;
using Store.Domain.StoreContext.ValueObjects;

namespace Store.Tests.Entities;
[TestClass]
public class OrderTests
{
    private Name _name;
    private Document _document;
    private Email _email;
    private Phone _phone;
    private Address _addressShipping;
    private Address _addressBilling;
    private Customer _customer;
    private Product _product1;
    private Product _product2;
    private Product _product3;
    private Product _product4;
    private Product _product5;
    private Product _product6;

    public OrderTests()
    {
        _name = new("Jonny", "Four");
        _document = new("04444");
        _email = new("jonny@four.qu4tro");
        _phone = new("123456789");

        _addressShipping = new(
       "Rua Comum de sempre",
       "4",
       "Bairro Quaren",
       "Casa",
       "Cidadenópolis",
       "PR",
       "Brasil",
       "19288070",
       EAddressType.Shipping
       );
        _addressBilling = new(
          "Rua Quatro quatros",
          "44",
          "Bairro dos Prédios",
          "Quadrante",
          "Townsville",
          "ZN",
          "China",
          "09790010",
          EAddressType.Billing
       );

        _customer = new(
            _name,
            _document,
            _email,
            _phone
        );

        _product1 = new(
            "Mouse",
            "Mouse Gamer 4You",
            "https://url-da-imagem",
            10,
            20
        );
        _product2 = new(
            "Teclado",
            "Teclado Gamer 4You",
            "https://url-da-imagem",
            18,
            22
        );
        _product3 = new(
            "Cadeira",
            "Cadeira Gamer 4You",
            "https://url-da-imagem",
            188.25M,
            20
        );
        _product4 = new(
            "4Phone",
            "Smartphone 4Phone",
            "https://url-da-imagem",
            1881.25M,
            209
        );
        _product5 = new(
            "Tablet 4Share",
            "Table 4Share Sharanco",
            "https://url-da-imagem",
            956.55M,
            17
        );
        _product6 = new(
            "Monitor 4Share 22p",
            "Monitor 4Share Sharanco",
            "https://url-da-imagem",
            916.15M,
            19
        );
    }

    [TestMethod]
    public void Test()
    {
        _customer.AddAddress(_addressShipping);
        _customer.AddAddress(_addressBilling);

        var order = new Order(_customer);
        order.AddItem(new OrderItem(_product1, 5));
        order.AddItem(new OrderItem(_product2, 1));
        order.AddItem(new OrderItem(_product3, 8));
        order.AddItem(new OrderItem(_product4, 12));
        order.AddItem(new OrderItem(_product5, 4));
        order.AddItem(new OrderItem(_product6, 1));

        // Gera o pedido
        order.PlaceOrder();

        // Simula o pagamento
        order.Pay();

        // Simula o envio
        order.Ship();

        // Simula o cancelamento
        order.Cancel();
    }
}
