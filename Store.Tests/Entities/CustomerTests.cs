using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Enums;
using Store.Domain.StoreContext.ValueObjects;

namespace Store.Tests.Entities;
[TestClass]
public class CustomerTests
{
    private Name _name;
    private Name _name2;
    private Document _document;
    private Email _email;
    private Phone _phone;
    private Address _addressShipping;
    private Address _addressBilling;
    private Customer _validCustomer;
    private Customer _invalidCustomer;

    public CustomerTests()
    {
        _name = new("John", "Doe");
        _name2 = new("A", "Norianz");

        _document = new("04444");
        _email = new("john@four.qu4tro");
        _phone = new("044444444");

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

        _validCustomer = new(
            _name,
            _document,
            _email,
            _phone
        );

        _invalidCustomer = new(
            _name2,
            _document,
            _email,
            _phone
        );

        _validCustomer.AddAddress(_addressShipping);
        _validCustomer.AddAddress(_addressBilling);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCustomerIsInValid()
    {
        Assert.IsFalse(_invalidCustomer.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCustomerIsValid()
    {
        Assert.IsTrue(_validCustomer.IsValid);
    }
}
