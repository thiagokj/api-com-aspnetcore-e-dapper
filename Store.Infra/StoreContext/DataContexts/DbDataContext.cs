using System.Data;
using System.Data.SqlClient;
using Store.Shared;

namespace Store.Infra.StoreContext.DataContexts;

public class DbDataContext : IDisposable
{
    public DbDataContext()
    {
        Connection = new SqlConnection(Settings.ConnectionString);
        Connection.Open();
    }

    public SqlConnection Connection { get; set; }

    public void Dispose()
    {
        if (Connection.State != ConnectionState.Closed)
            Connection.Close();
    }
}