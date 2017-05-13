﻿using SyncPrototype.Client;
using SyncPrototype.Connect;
using System;
using System.IO;

namespace SyncPrototype.Tests
{
    public class NewSyncTestRun : TestRun
    {
        public NewSyncTestRun(SmplRepository smpls, SampleRepository samples, ILogger writer) : base(smpls, samples, writer)
        {
        }

        public override string RunName => "Individual Saves - All Inserts Setup";

        protected override void PrepTest()
        {
            this.ConnectRepository.Reset();
        }

        protected override void Initialize()
        {
            if(this.ClientRepository.Count == 0)
            {
                var seeder = new Db.FirstTimeSyncSetup(ClientRepository);
                Writer.WriteLine("Seeding {0} new records. ", seeder.Count);

                seeder.Seed();
            }
        }
    }
}
