using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SyncPrototype.Components
{
    public class SqlConnectionFactory : IConnectionFactory, IDisposable
    {
        public IDbConnection Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["sync"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            
        }
    }

    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
