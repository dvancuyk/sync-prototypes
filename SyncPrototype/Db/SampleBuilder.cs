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
            var rng = new Random();

            var assessedValue = rng.Next(1, 3) * 100000;

            return new Sample
            {
                Id = id,
                Name = "Sample " + id,
                Description = Guid.NewGuid().ToString(),
                IsActive = true,
                Token = Guid.NewGuid(),
                AddressLine1 = id + " Main Street",
                AddressLine2 = "Suite " + id,
                City = "St. Petersburg",
                State = "FL",
                ZipCode = "33710",
                BuildingNumber = id.ToString(),
                Legal1 = Guid.NewGuid().ToString("N").Substring(30),
                Legal2 = Guid.NewGuid().ToString("N").Substring(30),
                SquareFootage = (rng.Next(10, 30) * 1000).ToString(),
                AssessedValue = "$" + assessedValue,
                OwnerRatio = "100",
                GroupOwnerNumber = id.ToString(),
                GlCostCenter = Guid.NewGuid().ToString("N").Substring(3),
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

        public static Sample[] Many(int count = 5, int? id = null)
        {
            return Enumerable
                .Range(1, count)
                .Select(current => Single(id ?? current))
                .ToArray();
        }
    }
}
