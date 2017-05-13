﻿using SyncPrototype.Client;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;

namespace SyncPrototype.Tests
{
    public class ModifiedSyncTestRun : TestRun
    {
        private ushort modifiedPercentage;

        public ModifiedSyncTestRun(SmplRepository smpls, SampleRepository samples, ILogger writerFactory, ushort modifiedPercentage = 50) 
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

        public override string RunName => $"Individual Saves - Updates with {ModifiedPercetage}% modified";
    }
}
