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
            if (obj == null)
                return false;

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
        public static bool operator ==(SampleIdentity left, SampleIdentity right)
        {
            if (Equals(left, null))
            {
                return (Equals(right, null)) ? true : false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(SampleIdentity x, SampleIdentity y)
        {
            return !(x == y);
        }
    }


}
