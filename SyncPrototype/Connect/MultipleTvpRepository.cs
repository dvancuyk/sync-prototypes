using Dapper;
using SyncPrototype.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SyncPrototype.Connect
{
    public class MultipleTvpRepository : IRepository<Sample>, IDisposable
    {
<<<<<<< HEAD:SyncPrototype/Connect/MultipleTvpRepository.cs
        private const string update = "Samples_Update";
        private const string insert = "Samples_Insert";
        private const string delete = "Samples_Delete";
=======
        private IDbConnection connection;
        private const string update = "Samples_Update";
        private const string insert = "Samples_Insert";
        private const string delete = "Samples_Delete";

        private List<Sample> modified = new List<Sample>();
        private List<Sample> netNew = new List<Sample>();
        private List<Sample> removed = new List<Sample>();
>>>>>>> phase-2:SyncPrototype/Connect/SampleRepository.cs

        private SampleTable modified = new SampleTable();
        private SampleTable netNew = new SampleTable();
        private SampleTable removed = new SampleTable();

        public MultipleTvpRepository(IConnectionFactory factory)
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
<<<<<<< HEAD:SyncPrototype/Connect/MultipleTvpRepository.cs
            using (var connection = Factory.Create())
            {
                connection.Query<Sample>("DELETE FROM dbo.ConnectSample"); 
=======
            if(sample.Id > 0)
            {
                modified.Add(sample);
            }
            else
            {
                netNew.Add(sample);
>>>>>>> phase-2:SyncPrototype/Connect/SampleRepository.cs
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

        private void Execute(string proc, SampleTable samples)
        {
            using (var connection = Factory.Create())
            {
                connection.Execute(proc, new { samples = samples.Table }, commandType: CommandType.StoredProcedure);
            }
            samples.Clear();
        }

        public void Delete(Sample entity)
        {
<<<<<<< HEAD:SyncPrototype/Connect/MultipleTvpRepository.cs
            removed.Add(entity, true);
=======
            Execute(update, modified);
            Execute(insert, netNew);
            Execute(delete, removed);
        }

        private void Execute(string proc, List<Sample> samples)
        {
            var executor = new StoredProcExecutor(Connection);
            executor.Execute(proc, samples);
            samples.Clear();
>>>>>>> phase-2:SyncPrototype/Connect/SampleRepository.cs
        }
    }
}
