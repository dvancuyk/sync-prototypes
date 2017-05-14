using SyncPrototype.Client;
using SyncPrototype.Connect;
using System.Collections.Generic;
using System.Linq;

namespace SyncPrototype.Components.Samples
{
    public class SampleProcessor : IProcessor
    {
        private IRepository<Smpl> dataSource;
        private IRepository<Sample> synced;

        public SampleProcessor(IRepository<Smpl> clientRepository, IRepository<Sample> connectRepository)
        {
            dataSource = clientRepository;
            synced = connectRepository;
            Comparer = new SampleDataComparer();
            Resolver = new ConnectConflictResolver();
            Mapper = new SampleConnectMapper();
        }

        public SampleDataComparer Comparer { get; set; }
        public ConnectConflictResolver Resolver { get; set; }
        public SampleConnectMapper Mapper { get; set; }

        public virtual void Process()
        {
            var records = dataSource
                .All();
            var synced = this.synced
                .All()
                .ToDictionary(SampleIdentity.Create);

            foreach (var record in records)
            {
                var identity = new SampleIdentity(record);
                if(!synced.ContainsKey(identity))
                {
                    this.synced.Save(Mapper.Convert(record));
                }
                else
                {
                    if (Comparer.HasChanged(synced[identity], record))
                        this.synced.Save(Mapper.Convert(record, synced[identity]));

                    synced.Remove(identity);
                }
            }

            // We delete any records that aren't found in the client. 
            // This obviously doesn't consider records which originate in Connect and need to be pushed to Client. That should be considered later.
            foreach (var item in synced)
            {
                this.synced.Delete(item.Value);
            }

            this.synced.Finish();
        }
    }

    public interface IProcessor
    {
        void Process();
    }
}
