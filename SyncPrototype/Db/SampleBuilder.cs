using SyncPrototype.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Db

{
    public static class SampleBuilder
    {
        public static Sample Single(int id = 1)
        {
            return new Sample
            {
                Id = id,
                Name = "Sample " + id,
                Description = Guid.NewGuid().ToString()
            };
        }

        public static Sample[] Many(int count = 5, int? id = null)
        {
            return Enumerable
                .Range(1, count)
                .Select(current => Single(id ?? current))
                .ToArray();
        }
    }
}
