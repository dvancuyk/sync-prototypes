using SyncPrototype.Client;
using SyncPrototype.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Components.Samples
{
    /// <summary>
    /// Converts a client side <see cref="Smpl"/> instance into the <see cref="Sample"/> equivalent.
    /// </summary>
    public class SampleConnectMapper
    {
        public Sample Convert(Smpl sample)
        {
            return Convert(sample, new Sample());
        }

        public Sample Convert(Smpl sample, Sample converted)
        {
            converted.Name = sample.Name;
            converted.Description = sample.Description;
            converted.Changed = false;

            return converted;
        }
    }
}
