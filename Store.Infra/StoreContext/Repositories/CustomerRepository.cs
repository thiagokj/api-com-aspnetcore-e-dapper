using System.Data;
using Dapper;
using Store.Domain.StoreContext.Entities;
using Store.Domain.StoreContext.Queries;
using Store.Domain.StoreContext.Repositories;
using Store.Infra.StoreContext.DataContexts;

namespace Store.Infra.StoreContext.Repositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly DbDataContext _context;

    public CustomerRepository(DbDataContext context) => _context = context;

    public bool CheckCustomer(Guid id)
    {
        var sql = @$"
            SELECT CASE WHEN EXISTS (
               SELECT [Id]
               FROM [Customer]
               WHERE [Id] = @Id
            )
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
            END";

        return _context
            .Connection
            .Query<bool>(
                sql,
                new { Id = id }
            )
            .FirstOrDefault();
    }

    public bool CheckDocument(string document)
    {
        return _context
            .Connection
            .Query<bool>(
                "spCheckDocument",
                new { Document = document },
                commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
    }

    public bool CheckEmail(string email)
    {
        return _context
            .Connection
            .Query<bool>(
                "spCheckEmail",
                new { Email = email },
                commandType: CommandType.StoredProcedure)
            .FirstOrDefault();
    }

    public bool CheckEmailForUpdate(Guid id, string email)
    {
        var sql = @$"
            SELECT CASE WHEN EXISTS (
                SELECT [Id]
                FROM [Customer]
                WHERE [Id] != @Id
                AND [Email] = @Email
            )
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
            END";

        return _context
        .Connection
        .Query<bool>(
            sql,
            new { Id = id, Email = email })
        .FirstOrDefault();
    }

    public void Save(Customer customer)
    {
        _context
        .Connection
        .Execute("spCreateCustomer",
        new
        {
            Id = customer.Id,
            FirstName = customer.Name.FirstName,
            LastName = customer.Name.LastName,
            Document = customer.Document.Number,
            Email = customer.Email.Address,
            Phone = customer.Phone.Number
        },
        commandType: CommandType.StoredProcedure);

        foreach (var address in customer.Addresses)
        {
            _context
            .Connection
            .Execute("spCreateAddress",
            new
            {
                Id = address.Id,
                CustomerId = customer.Id,
                Number = address.Number,
                Street = address.Street,
                Complement = address.Complement,
                Neighborhood = address.Neighborhood,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode,
                Type = address.Type
            },
            commandType: CommandType.StoredProcedure);
        }
    }

    public CustomerOrdersCountResult? GetCustomerOrdersCount(string document)
    {
        var sql = @$"
            SELECT
                C.[Id],
                CONCAT(C.[FirstName],' ',C.[LastName]) AS [Customer Name],
                C.[Document],
                COUNT(C.[Id]) AS [Quantity Orders]
            FROM 
                [Customer] AS C
            INNER JOIN
                [Order] AS O
            ON
                C.[Id] = O.[CustomerId]
            WHERE
                C.[Document] = @document  
            GROUP BY
                C.[Id],
                CONCAT(C.[FirstName],' ',C.[LastName]),
                C.[Document]";

        return _context
            .Connection
            .Query<CustomerOrdersCountResult>(
                sql,
                new { Document = document }
            ).FirstOrDefault();
    }

    public IEnumerable<ListCustomerQueryResult> Get()
    {
        var sql = @$"
            SELECT
                [Id],
                CONCAT([FirstName],' ',[LastName]) AS [Name],
                [Email],
                [Document]
            FROM 
                [Customer]";

        return _context
            .Connection
            .Query<ListCustomerQueryResult>(sql);
    }

    public GetCustomerQueryResult? Get(Guid id)
    {
        var sql = @$"
            SELECT
                [Id],
                CONCAT([FirstName],' ',[LastName]) AS [Name],
                [Email],
                [Document]
            FROM 
                [Customer]
            WHERE
                [Id] = @id";

        return _context
            .Connection
            .Query<GetCustomerQueryResult>(sql, new { id = id })
            .FirstOrDefault();
    }

    public IEnumerable<ListCustomerOrdersQueryResult> GetOrders(Guid id)
    {
        var sql = @$"
            SELECT
                C.[Id],
                CONCAT(C.[FirstName],' ',C.[LastName]) AS [Name],
                C.[Document],
                C.[Email],
                O.[Id] AS [OrderId],
                SUM(OI.[Price] * OI.[Quantity]) AS TotalOrder
            FROM 
                [Customer] AS C
            INNER JOIN
                [Order] AS O
            ON
                C.[Id] = O.[CustomerId]
            INNER JOIN
                [OrderItem] AS OI
            ON
                O.[Id] = OI.[OrderId]      
            WHERE
                C.[Id] = @Id
            GROUP BY    
                C.[Id],
                CONCAT(C.[FirstName],' ',C.[LastName]),
                C.[Document],
                C.[Email],
                O.[Id]";

        return _context
            .Connection
            .Query<ListCustomerOrdersQueryResult>(sql, new { Id = id });
    }

    public void Update(Customer customer)
    {
        var sql = @$"
            UPDATE [Customer]
            SET
                [FirstName] = @FirstName,
                [LastName] = @LastName,
                [Document] = @Document,
                [Email] = @Email,
                [Phone] = @Phone
            FROM 
                [Customer]
            WHERE
                [Id] = @id";

        _context
        .Connection
        .Execute(sql, new
        {
            Id = customer.Id,
            FirstName = customer.Name.FirstName,
            LastName = customer.Name.LastName,
            Document = customer.Document.Number,
            Email = customer.Email.Address,
            Phone = customer.Phone.Number
        }
        );
    }

    public void Delete(string id)
    {
        var sql = "DELETE FROM [Customer] WHERE [Id] = @id";
        _context.Connection.Execute(sql, new { Id = id });
    }

    public bool CheckDocumentForUpdate(Guid id, string document)
    {
        var sql = @$"
            SELECT CASE WHEN EXISTS (
                SELECT [Id]
                FROM [Customer]
                WHERE [Id] != @Id
                AND [Document] = @Document
            )
            THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
            END";

        return _context
        .Connection
        .Query<bool>(
            sql,
            new { Id = id, Document = document })
        .FirstOrDefault();
    }
}