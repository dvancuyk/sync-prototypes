using System.Collections.Generic;
using SyncPrototype.Components;
using System.Data;
using Dapper;
using System;

namespace SyncPrototype.Connect
{
    public class SingleTvpRepository : IRepository<Sample>
    {
        private IRepository<Sample> repo;
        private SampleTable changes = new SampleTable();

        public SingleTvpRepository(IRepository<Sample> repo)
        {
            this.repo = repo;
        }

        public int Count => repo.Count;

        public IConnectionFactory Factory => repo.Factory;

        public IEnumerable<Sample> All()
        {
            return repo.All();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public void Save(Sample entity)
        {
            changes.Add(entity);
        }

        public void Save(IEnumerable<Sample> entities)
        {
            foreach (var sample in entities)
            {
                changes.Add(sample);
            }
        }

        public void Finish()
        {
            using (var connection = Factory.Create())
            {
                connection.Execute("Samples_SaveCollection", new { samples = changes.Table }, commandType: CommandType.StoredProcedure);
            }
        }

        public void Reset()
        {
            repo.Reset();
        }

        public void Delete(Sample entity)
        {
            entity.Deleted = true;

        }
    }
}
