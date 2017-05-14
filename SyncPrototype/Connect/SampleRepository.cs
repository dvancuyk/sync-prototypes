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
                using (var connection = Factory.Create())
                {
                    return connection.Query<int>("SELECT COUNT(1) FROM dbo.ConnectSample").First(); 
                }
            }
        }

        public IConnectionFactory Factory { get; }

        public void Dispose()
        {

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
            using (var connection = Factory.Create())
            {
                return connection.Query<Sample>("SELECT * FROM dbo.ConnectSample"); 
            }
        }

        public void Reset()
        {
            using (var connection = Factory.Create())
            {
                connection.Query<Sample>("DELETE FROM dbo.ConnectSample"); 
            }
        }

        public void Finish()
        {
            using (var connection = Factory.Create())
            {
                Execute(update, modified);
                Execute(insert, netNew);
                Execute(delete, removed);

            }
        }

        private void Execute(string proc, List<Sample> samples)
        {
            using (var connection = Factory.Create())
            {
                var executor = new StoredProcExecutor(connection);
                executor.Execute(proc, samples); 
            }
            samples.Clear();
        }
    }
}
