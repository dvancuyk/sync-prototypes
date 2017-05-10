using SyncPrototype.Client;
using SyncPrototype.Connect;
using SyncPrototype.Db;

namespace SyncPrototype.Tests
{
    public class ModifiedSyncTestRun : TestRun
    {
        private ushort modifiedPercentage;

        public ModifiedSyncTestRun(SmplRepository smpls, SampleRepository samples, ILogger writer, ushort modifiedPercentage = 50) 
            : base(smpls, samples, writer)
        { 
            ModifiedPercetage = modifiedPercentage;
        }

        public ushort ModifiedPercetage
        {
            get { return modifiedPercentage; }
            set
            {
                if (modifiedPercentage > 100)
                    modifiedPercentage = 100;
            }
        }

        protected override void PrepTest()
        {
            new PartialUpdates(ConnectRepository, ModifiedPercetage).Seed();
        }

        public override string RunName => $"Sync with {ModifiedPercetage}% modified";
    }
}
