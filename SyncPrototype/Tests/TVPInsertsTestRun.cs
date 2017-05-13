using SyncPrototype.Client;
using SyncPrototype.Connect;
using System;

namespace SyncPrototype.Tests
{
    public class TVPInsertsTestRun : TestRun
    {

        public TVPInsertsTestRun(SmplRepository smpls, SampleRepository samples, ILogger writer) 
            : base(smpls, new TvpSampleRepository(samples), writer)
        {
        }

        public override string RunName => "TVP All Inserts Test Run";


        protected override void PrepTest()
        {
            this.ConnectRepository.Reset();
        }

        protected override void Initialize()
        {
            if (this.ClientRepository.Count == 0)
            {
                var seeder = new Db.FirstTimeSyncSetup(ClientRepository);
                Writer.WriteLine("Seeding {0} new records. ", seeder.Count);

                seeder.Seed();
            }
        }
    }
}
