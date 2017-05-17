using SyncPrototype.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Client
{
    public class Smpl : ISampleType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid Token { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string BuildingNumber { get; set; }
        public string Legal1 { get; set; }
        public string Legal2 { get; set; }
        public string SquareFootage { get; set; }
        public string AssessedValue { get; set; }
        public string OwnerRatio { get; set; }
        public string GroupOwnerNumber { get; set; }
        public string GlCostCenter { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GeocodeProvider { get; set; }
        public string GeocodeAccuracy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PROASSMTCATG { get; set; }
        public decimal PROASSMTAMT { get; set; }
        public decimal PROASSESSEDVAL { get; set; }
    }
}
