using Dapper;
using SyncPrototype.Client;
using SyncPrototype.Components;
using System;

namespace SyncPrototype.Db
{
    public class ChangeSmplRepresentation
    {
        private readonly IRepository<Smpl> samples;
        private ushort percentage;

        public ChangeSmplRepresentation(IRepository<Smpl> sampleRepository)
        {
            this.samples = sampleRepository;
        }

        public void Modify(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = total * percentage / 100;
            var description = $"To Be Synced ({DateTime.Now.ToLongTimeString()})";
            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"UPDATE [dbo].[ClientSmpl] SET Description = '{description}' WHERE Name IN(SELECT TOP {changedAmount} Name FROM ClientSmpl)");
            }
        }

        public void Insert(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = total * percentage / 100;
            var current = SmpleBuilder.Many(changedAmount, total + 1);

            foreach (var sample in current)
            {
                samples.Save(sample);
            }

            samples.Finish();
        }

        public void Remove(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = total * percentage / 100;

            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"DELETE TOP ({changedAmount}) FROM [dbo].[ClientSmpl]");
            }

        }
    }
}
