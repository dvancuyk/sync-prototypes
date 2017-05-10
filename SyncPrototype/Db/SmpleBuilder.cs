using SyncPrototype.Client;
using System;
using System.Linq;

namespace SyncPrototype.Db
{
    public static class SmpleBuilder
    {
        public static Smpl Single(int id)
        {
            return new Smpl
            {
                Name = "Sample " + id,
                Description = Guid.NewGuid().ToString()
            };
        }

        public static Smpl[] Many(int count = 5)
        {
            return Enumerable
                .Range(1, count)
                .Select(Single)
                .ToArray();
        }
    }
}
