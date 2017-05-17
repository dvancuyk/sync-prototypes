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
            converted.IsActive = sample.IsActive;
            converted.Token = sample.Token;
            converted.AddressLine1 = sample.AddressLine1;
            converted.AddressLine2 = sample.AddressLine2;
            converted.City = sample.City;
            converted.State = sample.State;
            converted.ZipCode = sample.ZipCode;
            converted.BuildingNumber = sample.BuildingNumber;
            converted.Legal1 = sample.Legal1;
            converted.Legal2 = sample.Legal2;
            converted.SquareFootage = sample.SquareFootage;
            converted.AssessedValue = sample.AssessedValue;
            converted.OwnerRatio = sample.OwnerRatio;
            converted.GroupOwnerNumber = sample.GroupOwnerNumber;
            converted.GlCostCenter = sample.GlCostCenter;
            converted.Latitude = sample.Latitude;
            converted.Longitude = sample.Longitude;
            converted.GeocodeProvider = sample.GeocodeProvider;
            converted.GeocodeAccuracy = sample.GeocodeAccuracy;
            converted.ModifiedDate = sample.ModifiedDate;
            converted.PROASSMTCATG = sample.PROASSMTCATG;
            converted.PROASSMTAMT = sample.PROASSMTAMT;
            converted.PROASSESSEDVAL = sample.PROASSESSEDVAL;
            converted.Changed = false;

            return converted;
        }
    }
}
