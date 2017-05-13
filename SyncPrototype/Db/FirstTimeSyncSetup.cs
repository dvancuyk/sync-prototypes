using SyncPrototype.Client;
using System.Data;

namespace SyncPrototype.Db
{
    public class FirstTimeSyncSetup
    {
        private readonly SmplRepository repository;
        public int Count { get; set; }

        public FirstTimeSyncSetup(SmplRepository smplRepository)
        {
            this.repository = smplRepository;
            Count = 10;//30000;
        }

           

        public void Seed()
        {
            foreach (var item in SmpleBuilder.Many(Count))
            {
                repository.Save(item);   
            }
        }
    }
}
