using Dapper;
using SyncPrototype.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SyncPrototype.Client
{
    public class SmplRepository : IRepository<Smpl>
    {
        private IDbConnection connection;
        private List<Smpl> samples = new List<Smpl>();

        public int Count
        {
            get
            {
                using (var connection = Factory.Create())
                {
                    return connection.Query<int>("SELECT COUNT(1) FROM dbo.ClientSmpl").First(); 
                }
            }
        }

        public SmplRepository(IConnectionFactory factory)
        {
            this.Factory = factory;
        }

        public IConnectionFactory Factory { get; }
        

        public void Save(Smpl sample)
        {
            samples.Add(sample);
        }

        public IEnumerable<Smpl> All()
        {
            using (var connection = Factory.Create())
            {
                return connection.Query<Smpl>("SELECT * FROM dbo.ClientSmpl"); 
            }
        }

        public void Finish()
        {
            using (var connection = Factory.Create())
            {
                connection.Execute("Smpls_SaveCollection", new { samples = BuildTable() }, commandType: CommandType.StoredProcedure); 
            }
            samples.Clear();
        }

        private DataTable BuildTable()
        {

            var table = new DataTable("SampleType");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Description", typeof(string));

            foreach (var sample in samples)
            {
                var row = table.Rows.Add(0, sample.Name, sample.Description);
            }

            return table;
        }

        public void Reset()
        {
            using (var connection = Factory.Create())
            {
                connection.Execute("DELETE FROM dbo.ClientSmpl"); 
            }
        }

        public void Dispose()
        {
            
        }
    }
}
