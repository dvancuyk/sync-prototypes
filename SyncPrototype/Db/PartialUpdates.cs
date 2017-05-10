using SyncPrototype.Client;
using SyncPrototype.Connect;
using System;
using System.IO;

namespace SyncPrototype.Db
{
    public class PartialUpdates
    {
        private readonly SampleRepository samples;
        private ushort percentage;

        public PartialUpdates(SampleRepository sampleRepository, ushort percent)
        {
            this.samples = sampleRepository;
            if (percent > 100) percent = 100;

            percentage = percent;
        }
        
        public void Seed()
        {
            int count = 0;
            int taken = 0;
            foreach (var item in samples.All())
            {
                if (taken < percentage)
                {
                    item.Changed = true;
                    item.Description = Guid.NewGuid().ToString();

                    taken++;
                    samples.Save(item);
                }
                count++;

                if (count > 100)
                {
                    count = 0;
                    taken = 0;
                }
            }
        }
    }
}
