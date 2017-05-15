using Dapper;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SyncPrototype.Client
{
    public class SmplRepository : IRepository<Smpl>
    {
        private SampleTable samples = new SampleTable();

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
                connection.Execute("Smpls_SaveCollection", new { samples = samples.Table }, commandType: CommandType.StoredProcedure); 
            }
            samples.Clear();
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

        public void Delete(Smpl entity)
        {
            using (var connection = Factory.Create())
            {
                connection.Execute("DELETE FROM dbo.ClientSmpl WHERE [Name] = @Name", entity);
            }
        }
    }
}
