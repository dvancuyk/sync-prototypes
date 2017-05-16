using SyncPrototype.Components;
using System.Collections.Generic;
using System.Linq;

namespace SyncPrototype.Connect
{
    /// <summary>
    /// Repository instance which tries to mimic at best how the current sync application pushes data to the underlying data store, which is
    /// push everything, let the DB figure it out.
    /// </summary>
    public class TraditionalSampleRepository : IRepository<Sample>
    {
        private IRepository<Sample> repo;
        private Dictionary<int, Sample> samples = new Dictionary<int, Sample>();

        public TraditionalSampleRepository(IConnectionFactory factory)
        {
            this.repo = new MultipleTvpRepository(factory);
        }

        public int Count => repo.Count;

        public IConnectionFactory Factory => repo.Factory;

        public IEnumerable<Sample> All()
        {
            if (!samples.Any())
            {
                samples = repo.All().ToDictionary(s => s.Id);
            }
            return samples.Select(s => s.Value);
        }

        public void Delete(Sample sample)
        {
            samples.Remove(sample.Id);
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public void Save(Sample entity)
        {
            samples.Add(entity.Id, entity);
        }

        public void Save(IEnumerable<Sample> entities)
        {
            using (var connection = Factory.Create())
            {
                var storedProcCommand = new StoredProcExecutor(connection);
                storedProcCommand.Execute("[dbo].[Samples_Sync]", entities);
            }
        }

        public void Finish()
        {
            Save(All());
            samples.Clear();
        }

        public void Reset()
        {
            repo.Reset();
        }
    }
}
