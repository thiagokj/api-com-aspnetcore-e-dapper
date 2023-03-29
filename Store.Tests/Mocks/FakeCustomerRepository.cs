using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Queries;
using Store.Domain.StoreContext.Repositories;

namespace Store.Tests.Mocks;
public class FakeCustomerRepository : ICustomerRepository
{
    public bool CheckDocument(string document)
    {
        return false;
    }

    public bool CheckEmail(string email)
    {
        return false;
    }

    public bool CheckCustomer(Guid id)
    {
        return false;
    }

    public IEnumerable<ListCustomerQueryResult> Get()
    {
        throw new NotImplementedException();
    }

    public GetCustomerQueryResult? Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public CustomerOrdersCountResult? GetCustomerOrdersCount(string document)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Save(Customer customer)
    {

    }

    public void Update(Customer customer)
    {
        throw new NotImplementedException();
    }

    public bool CheckDocumentForUpdate(Guid id, string document)
    {
        throw new NotImplementedException();
    }

    public bool CheckEmailForUpdate(Guid id, string email)
    {
        throw new NotImplementedException();
    }

    public void Delete(string id)
    {
        throw new NotImplementedException();
    }
}