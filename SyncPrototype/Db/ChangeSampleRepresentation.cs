using SyncPrototype.Components;
using SyncPrototype.Connect;
using Dapper;

namespace SyncPrototype.Db
{
    public class ChangeSampleRepresentation
    {
        private readonly IRepository<Sample> samples;
        private ushort percentage;

        public ChangeSampleRepresentation(IRepository<Sample> sampleRepository)
        {
            this.samples = sampleRepository;
        }

        public ChangeSampleRepresentation(IRepository<Sample> sampleRepository, ushort modifiedAmount)
        {
            this.samples = sampleRepository;
            if (modifiedAmount > 100) modifiedAmount = 100;

            percentage = modifiedAmount;
        }
        
        public void Modify(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = total * percentage / 100;
            var description = "Out of sync";
            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"UPDATE [dbo].[ConnectSample] SET Description = '{description}' WHERE Id IN(SELECT TOP {changedAmount} Id FROM ConnectSample)");
            }
        }

        public void Insert(Percentage percentage)
        {
            var total = samples.Count;
            var changedAmount = total * percentage / 100;
            var current = SampleBuilder.Many(changedAmount, 0);

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
                connection.Execute($"DELETE TOP ({changedAmount}) FROM [dbo].[ConnectSample]");
            }

        }
    }
}
