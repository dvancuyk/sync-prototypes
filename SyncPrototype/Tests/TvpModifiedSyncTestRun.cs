using SyncPrototype.Client;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Tests
{
    public class TvpModifiedSyncTestRun : TestRun
    {
        private ushort modifiedPercentage;

        public TvpModifiedSyncTestRun(SmplRepository smpls, SampleRepository samples, ILogger writerFactory, ushort modifiedPercentage = 50) 
            : base(smpls, samples, writerFactory)
        {
            ModifiedPercetage = modifiedPercentage;
        }

        public ushort ModifiedPercetage
        {
            get { return modifiedPercentage; }
            set
            {
                modifiedPercentage = value;
                if (modifiedPercentage > 100)
                    modifiedPercentage = 100;
            }
        }

        protected override void PrepTest()
        {
            new PartialUpdates(ConnectRepository, ModifiedPercetage).Seed();
        }

        public override string RunName => $"TVP Updates with {ModifiedPercetage}% modified";
    }
}

