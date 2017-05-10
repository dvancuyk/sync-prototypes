﻿using SyncPrototype.Client;
using SyncPrototype.Connect;
using System.IO;

namespace SyncPrototype.Tests
{
    public class NewSyncTests : TestRun
    {
        public NewSyncTests(SmplRepository smpls, SampleRepository samples, TextWriter writer) : base(smpls, samples, writer)
        {
        }

        public override string RunName => "New Sync Setup";

        protected override void PrepTest()
        {
            this.ConnectRepository.Reset();
        }

        protected override void Initialize()
        {
            if(this.ClientRepository.Count == 0)
            {
                new Db.FirstTimeSyncSetup(ClientRepository).Seed();
            }
        }
    }
}
