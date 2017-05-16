using SyncPrototype.Client;
using SyncPrototype.Connect;
using System.Linq;

namespace SyncPrototype.Components.Samples
{
    public class TraditionalSyncProcessor : SampleProcessor
    {
        public TraditionalSyncProcessor(IRepository<Smpl> clientRepository) 
            : base(clientRepository, new TraditionalSampleRepository(clientRepository.Factory))
        {
        }

        public override void Process()
        {
            var samples = base.dataSource.All();
            var repository = (TraditionalSampleRepository)synced;
            repository.Save(samples.Select(Mapper.Convert));
            repository.Finish();
        }
    }
}
