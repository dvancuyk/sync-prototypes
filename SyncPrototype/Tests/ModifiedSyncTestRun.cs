using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;
using System.Text;

namespace SyncPrototype.Tests
{
    public class ModifiedSyncTestRun : TestRun
    {
        public ModifiedSyncTestRun(SmplRepository smpls, IRepository<Sample> samples, ILogger writerFactory, ushort modifiedPercentage = 50) 
            : base(smpls, samples, writerFactory)
        {
            Modifications = modifiedPercentage;
            Inserts = 0;
            Deletes = 0;
        }

        public Percentage Modifications { get; set; }
        public Percentage Inserts { get; set; }
        public Percentage Deletes { get; set; }

        protected override void PrepTest()
        {
            var modifications = new ChangeSmplRepresentation(ClientRepository);


            if(Deletes > 0)
            {
                modifications.Remove(Deletes);
            }

            if (Modifications > 0)
            {
                modifications.Modify(Modifications);
            }

            if (Inserts > 0)
            {
                modifications.Insert(Inserts);
            }

        }

        public override string RunName
        {
            get
            {
                var builder = new StringBuilder(RepositoryName);
                builder.Append(" - ");

                bool hasPreviousPercentages = false;
                if(Deletes > 0)
                {
                    builder.Append(Deletes + " deleted");
                    hasPreviousPercentages = true;
                }

                if (Inserts > 0)
                {
                    if (hasPreviousPercentages) builder.Append(", ");
                    builder.Append(Inserts + " inserted");
                    hasPreviousPercentages = true;
                }

                if (Modifications > 0)
                {
                    if (hasPreviousPercentages) builder.Append(", ");
                    builder.Append(Modifications + " modified");
                }

                builder.Append(".");

                return builder.ToString();
            }
        }
    }
}
