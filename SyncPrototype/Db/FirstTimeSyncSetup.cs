using SyncPrototype.Client;
using System.Data;

namespace SyncPrototype.Db
{
    public class FirstTimeSyncSetup
    {
        private readonly SmplRepository repository;

        public FirstTimeSyncSetup(SmplRepository smplRepository)
        {
            this.repository = smplRepository;
        }

        public void Seed()
        {
            foreach (var item in SmpleBuilder.Many(100000))
            {
                repository.Save(item);   
            }
        }
    }
}
