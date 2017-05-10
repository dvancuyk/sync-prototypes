using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SyncPrototype.Components
{
    public class SqlConnectionFactory : IConnectionFactory
    {
        public IDbConnection Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["sync"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }

    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
