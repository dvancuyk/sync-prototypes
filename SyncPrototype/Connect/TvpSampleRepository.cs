using System.Collections.Generic;
using SyncPrototype.Components;
using System.Data;
using Dapper;
using System;

namespace SyncPrototype.Connect
{
    public class TvpSampleRepository : IRepository<Sample>
    {
        private IRepository<Sample> repo;
        private List<Sample> samples = new List<Sample>();

        public TvpSampleRepository(IRepository<Sample> repo)
        {
            this.repo = repo;
        }

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
            samples.Add(entity);
        }

        public void Save(IEnumerable<Sample> entities)
        {
            var storedProcCommand = new StoredProcExecutor(Factory.Create());
            storedProcCommand.Execute("Samples_SaveCollection", entities);
        }

        public void Finish()
        {
            Save(samples);
            samples.Clear();
        }

        public void Reset()
        {
            repo.Reset();
        }
    }
}
