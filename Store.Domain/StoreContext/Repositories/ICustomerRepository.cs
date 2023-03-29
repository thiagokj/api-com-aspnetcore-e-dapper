using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Queries;

namespace Store.Domain.StoreContext.Repositories;
public interface ICustomerRepository
{
    bool CheckCustomer(Guid id);
    bool CheckDocument(string document);
    bool CheckDocumentForUpdate(Guid id, string document);
    bool CheckEmail(string email);
    bool CheckEmailForUpdate(Guid id, string email);
    void Save(Customer customer);
    void Update(Customer customer);
    void Delete(string id);
    CustomerOrdersCountResult? GetCustomerOrdersCount(string document);
    IEnumerable<ListCustomerQueryResult> Get();
    GetCustomerQueryResult? Get(Guid id);
    IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id);
}