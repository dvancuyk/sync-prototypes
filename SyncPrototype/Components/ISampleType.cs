using System;


namespace SyncPrototype.Components
{
    public interface ISampleType
    {
        string Name { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }

        Guid Token { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string ZipCode { get; set; }
        string BuildingNumber { get; set; }
        string Legal1 { get; set; }
        string Legal2 { get; set; }
        string SquareFootage { get; set; }
        string AssessedValue { get; set; }
        string OwnerRatio { get; set; }
        string GroupOwnerNumber { get; set; }
        string GlCostCenter { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        string GeocodeProvider { get; set; }
        string GeocodeAccuracy { get; set; }
        DateTime ModifiedDate { get; set; }
        int PROASSMTCATG { get; set; }
        decimal PROASSMTAMT { get; set; }
        decimal PROASSESSEDVAL { get; set; }
    }
}
