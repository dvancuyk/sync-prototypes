using SyncPrototype.Client;
using System;
using System.Linq;

namespace SyncPrototype.Db
{
    public static class SmpleBuilder
    {
        public static Smpl Single(int id)
        {
            var rng = new Random();

            var assessedValue = rng.Next(1, 3) * 100000;

            return new Smpl
            {
                Name = "Sample " + id,
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                Token = Guid.NewGuid(),
                AddressLine1 = id + " Main Street",
                AddressLine2 = "Suite " + id,
                City = "St. Petersburg",
                State = "FL",
                ZipCode = "33710",
                BuildingNumber = Guid.NewGuid().ToString("N").Substring(0, 5),
                Legal1 = Guid.NewGuid().ToString("N").Substring(0, 30),
                Legal2 = Guid.NewGuid().ToString("N").Substring(0, 30),
                SquareFootage = (rng.Next(10, 30) * 1000).ToString(),
                AssessedValue = "$" + assessedValue,
                OwnerRatio = "100",
                GroupOwnerNumber = (id % 10).ToString(),
                GlCostCenter = Guid.NewGuid().ToString("N").Substring(0, 3),
                Latitude = 27 + rng.NextDouble(),
                Longitude = -29.1 + rng.NextDouble(),
                GeocodeProvider = "Google",
                GeocodeAccuracy = "BDE",
                ModifiedDate = DateTime.Now,
                PROASSMTCATG = id,
                PROASSMTAMT = assessedValue,
                PROASSESSEDVAL = assessedValue
            };
        }

        public static Smpl[] Many(int count = 5, int start = 0)
        {
            return Enumerable
                .Range(1, count)
                .Select(current => Single(start + current))
                .ToArray();
        }
    }
}
