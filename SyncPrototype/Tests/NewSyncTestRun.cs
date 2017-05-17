using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Connect;

namespace SyncPrototype.Tests
{
    public class NewSyncTestRun : TestRun
    {
        public NewSyncTestRun(SmplRepository smpls, IRepository<Sample> samples, ILogger writer) : base(smpls, samples, writer)
        {
        }

        public override string RunName => $"{RepositoryName} - All Inserts Setup";
        public bool ForceReset { get; set; }
        public int SeedCount { get; set; } = 30000;

        protected override void PrepTest()
        {
            this.ConnectRepository.Reset();
        }

        protected override void Initialize()
        {
            if (ForceReset)
                ClientRepository.Reset();

            if(this.ClientRepository.Count == 0)
            {
                var seeder = new Db.FirstTimeSyncSetup(ClientRepository)
                {
                    Count = SeedCount
                };
                Writer.WriteLine("Seeding {0} new records. ", seeder.Count);

                seeder.Seed();
            }

            ConnectRepository.Reset();
        }
    }
}
