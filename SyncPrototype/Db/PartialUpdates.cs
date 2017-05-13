using SyncPrototype.Components;
using SyncPrototype.Connect;

using System.Linq;

using Dapper;
using System;

namespace SyncPrototype.Db
{
    public class PartialUpdates
    {
        private readonly IRepository<Sample> samples;
        private ushort percentage;

        public PartialUpdates(IRepository<Sample> sampleRepository, ushort percent)
        {
            this.samples = sampleRepository;
            if (percent > 100) percent = 100;

            percentage = percent;
        }
        
        public void Seed()
        {
            var total = samples.All().Count();
            var changedAmount = total * percentage / 100;
            var description = "Modified on " + DateTime.Now.ToShortTimeString();
            using (var connection = samples.Factory.Create())
            {
                connection.Execute($"UPDATE [dbo].[ConnectSample] SET Description = '{description}' WHERE Id IN(SELECT TOP {changedAmount} Id FROM ConnectSample)");
            }
        }
    }
}
