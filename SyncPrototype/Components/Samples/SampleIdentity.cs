using SyncPrototype.Client;
using SyncPrototype.Connect;
using System;

namespace SyncPrototype.Components.Samples
{
    public class SampleIdentity : IComparable
    {
        private string name;

        public SampleIdentity(Sample sample)
        {
            name = sample.Name;
        }

        public SampleIdentity(Smpl sample)
        {
            name = sample.Name;
        }

        public static SampleIdentity Create(Sample sample)
        {
            return new SampleIdentity(sample);
        }

        public int CompareTo(object obj)
        {
            var identity = obj as SampleIdentity;

            if (identity == null)
                throw new InvalidOperationException("The type must be of SampleIdentity");

            return name.CompareTo(identity.name);
        }

        public override bool Equals(object obj)
        {
            var identity = obj as SampleIdentity;

            if (identity == null)
                return false;

            return name == identity.name;
        }

        public override int GetHashCode()
        {
            var hash = 17 * 31 + name.GetHashCode();
            return hash;
        }
        public static bool operator ==(SampleIdentity x, SampleIdentity y)
        {
            if (x == null || y == null) return false;

            return x.Equals(y);
        }

        public static bool operator !=(SampleIdentity x, SampleIdentity y)
        {
            return !(x == y);
        }
    }


}
