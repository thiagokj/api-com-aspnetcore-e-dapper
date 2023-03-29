using Flunt.Validations;
using Store.Domain.StoreContext.Entities;

namespace Store.Domain.StoreContext.Contracts;
public class CustomerContract : Contract<Customer>
{
    public CustomerContract(Customer customer)
    {
        Requires()
        .IsNotNull(customer.Name, "CustomerContract.customer.Name")
        .AreEquals(customer.Name.Notifications.Count, 0, "CustomerContract")
        .IsNotNull(customer.Document, "CustomerContract.customer.Document")
        .IsNotNull(customer.Email, "CustomerContract.customer.Email")
        .IsNotNull(customer.Phone, "CustomerContract.customer.Phone");
    }
}