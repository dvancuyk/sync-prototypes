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
        private const string update = "Samples_Update";
        private const string insert = "Samples_Insert";
        private const string delete = "Samples_Delete";

        private List<Sample> modified = new List<Sample>();
        private List<Sample> netNew = new List<Sample>();
        private List<Sample> removed = new List<Sample>();

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
            if(sample.Id > 0)
            {
                modified.Add(sample);
            }
            else
            {
                netNew.Add(sample);
            }
        }

        public IEnumerable<Sample> All()
        {
            return Connection.Query<Sample>("SELECT * FROM dbo.ConnectSample");
        }

        public void Reset()
        {
            Connection.Query<Sample>("DELETE FROM dbo.ConnectSample");
        }

        public void Finish()
        {
            Execute(update, modified);
            Execute(insert, netNew);
            Execute(delete, removed);
        }

        private void Execute(string proc, List<Sample> samples)
        {
            var executor = new StoredProcExecutor(Connection);
            executor.Execute(proc, samples);
            samples.Clear();
        }
    }
}
