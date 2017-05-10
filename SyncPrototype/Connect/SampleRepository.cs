using Dapper;
using SyncPrototype.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SyncPrototype.Connect
{
    public class SampleRepository : IRepository<Sample>, IDisposable
    {
        private IDbConnection connection;
        private const string update = "UPDATE dbo.ConnectSample SET Description = @Description, Name = @Name WHERE ID = @Key";
        private const string insert = "INSERT INTO dbo.ConnectSample (Name, Description) VALUES (@Name, @Description)";

        public SampleRepository(IConnectionFactory factory)
        {
            this.Factory = factory;
        }

        public int Count
        {
            get
            {
                return Connection.Query<int>("SELECT COUNT(1) FROM dbo.ConnectSample").First();
            }
        }

        public IConnectionFactory Factory { get; }
        private IDbConnection Connection
        {
            get
            {
                return connection ?? (connection = Factory.Create());
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        public void Save(Sample sample)
        {
            var command = sample.Key > 0 ? update : insert;
            Connection.Execute(command, sample);
        }

        public IEnumerable<Sample> All()
        {
            return Connection.Query<Sample>("SELECT * FROM dbo.ConnectSample");
        }

        public void Reset()
        {
            Connection.Query<Sample>("DELETE FROM dbo.ConnectSample");
        }
    }
}
