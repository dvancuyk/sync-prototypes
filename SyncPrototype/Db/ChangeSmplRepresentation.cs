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
            var description = $"To Be Synced ({DateTime.Now.ToLongTimeString()})";
            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"UPDATE [dbo].[ClientSmpl] SET Description = '{description}' WHERE Name IN(SELECT TOP {percentage.ChangeCount(samples.Count)} Name FROM ClientSmpl)");
            }
        }

        public void Insert(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = percentage.ChangeCount(total);
            
            var current = SmpleBuilder.Many(changedAmount, total * 100 + new Random().Next(1, 1000));

            foreach (var sample in current)
            {
                samples.Save(sample);
            }

            samples.Finish();
        }

        public void Remove(Percentage percentage)
        {
            var total = samples.Count;
            
            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"DELETE TOP ({percentage.ChangeCount(samples.Count)}) FROM [dbo].[ClientSmpl]");
            }

        }
    }
}
