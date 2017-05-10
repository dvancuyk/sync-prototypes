using System;
using System.Collections.Generic;
using SyncPrototype.Components;

namespace SyncPrototype.Connect
{
    public class TvpSampleRepository : IRepository<Sample>
    {
        private IRepository<Sample> repo;

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
            repo.Save(entity);
        }

        public void Save(IEnumerable<Sample> entities)
        {
            // see http://stackoverflow.com/questions/6232978/does-dapper-support-sql-2008-table-valued-parameters
        }
    }
}
