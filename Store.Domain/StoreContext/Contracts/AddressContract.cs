using Flunt.Validations;
using Store.Domain.StoreContext.Entities;

namespace Store.Domain.StoreContext.Contracts;
public class AddressContract : Contract<Address>
{
    public AddressContract(Address address)
    {
        Requires()
        .IsNotNullOrEmpty(address.Street, "AddressContract.address.Street")
        .IsNotNullOrEmpty(address.Number, "AddressContract.address.Number")
        .IsNotNullOrEmpty(address.Neighborhood, "AddressContract.address.Neighborhood")
        .IsNotNullOrEmpty(address.City, "AddressContract.address.City")
        .IsNotNullOrEmpty(address.State, "AddressContract.address.State")
        .IsNotNullOrEmpty(address.Country, "AddressContract.address.Country");
    }
}