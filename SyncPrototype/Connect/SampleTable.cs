using SyncPrototype.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace SyncPrototype.Connect
{
    public class SampleTable
    {

        public SampleTable()
        {
            Table = new DataTable("SampleType");
            Table.Columns.Add("Id", typeof(int));
            Table.Columns.Add("Name", typeof(string));
            Table.Columns.Add("Description", typeof(string));
            Table.Columns.Add("Delete", typeof(bool));
            Table.Columns.Add("IsActive", typeof(bool));
            Table.Columns.Add("Token", typeof(Guid));
            Table.Columns.Add("AddressLine1", typeof(string));
            Table.Columns.Add("AddressLine2", typeof(string));
            Table.Columns.Add("City", typeof(string));
            Table.Columns.Add("State", typeof(string));
            Table.Columns.Add("ZipCode", typeof(string));
            Table.Columns.Add("BuildingNumber", typeof(string));
            Table.Columns.Add("Legal1", typeof(string));
            Table.Columns.Add("Legal2", typeof(string));
            Table.Columns.Add("SquareFootage", typeof(string));
            Table.Columns.Add("AssessedValue", typeof(string));
            Table.Columns.Add("OwnerRatio", typeof(string));
            Table.Columns.Add("GroupOwnerNumber", typeof(string));
            Table.Columns.Add("GlCostCenter", typeof(string));
            Table.Columns.Add("Latitude", typeof(double));
            Table.Columns.Add("Longitude", typeof(double));
            Table.Columns.Add("GeocodeProvider", typeof(string));
            Table.Columns.Add("GeocodeAccuracy", typeof(string));
            Table.Columns.Add("ModifiedDate", typeof(string));
            Table.Columns.Add("PROASSMTCATG", typeof(int));
            Table.Columns.Add("PROASSMTAMT", typeof(decimal));
            Table.Columns.Add("PROASSESSEDVAL", typeof(decimal));

        }

        internal DataTable Table { get; }

        public void Add(Sample sample, bool remove = false)
        {
            Table.Rows.Add(
                sample.Id,
                sample.Name,
                sample.Description,
                remove,
                sample.IsActive,
                sample.Token,
                sample.AddressLine1,
                sample.AddressLine2,
                sample.City,
                sample.State,
                sample.ZipCode,
                sample.BuildingNumber,
                sample.Legal1,
                sample.Legal2,
                sample.SquareFootage,
                sample.AssessedValue,
                sample.OwnerRatio,
                sample.GroupOwnerNumber,
                sample.GlCostCenter,
                sample.Latitude,
                sample.Longitude,
                sample.GeocodeProvider,
                sample.GeocodeAccuracy,
                sample.ModifiedDate,
                sample.PROASSMTCATG,
                sample.PROASSMTAMT,
                sample.PROASSESSEDVAL);
        }

        public void Add(Smpl sample, bool remove = false)
        {
            Table.Rows.Add(0,
                sample.Name,
                sample.Description,
                remove,
                sample.IsActive,
                sample.Token,
                sample.AddressLine1,
                sample.AddressLine2,
                sample.City,
                sample.State,
                sample.ZipCode,
                sample.BuildingNumber,
                sample.Legal1,
                sample.Legal2,
                sample.SquareFootage,
                sample.AssessedValue,
                sample.OwnerRatio,
                sample.GroupOwnerNumber,
                sample.GlCostCenter,
                sample.Latitude,
                sample.Longitude,
                sample.GeocodeProvider,
                sample.GeocodeAccuracy,
                sample.ModifiedDate,
                sample.PROASSMTCATG,
                sample.PROASSMTAMT,
                sample.PROASSESSEDVAL);
        }

        public void AddRange(IEnumerable<Sample> samples)
        {
            if (samples == null) return;

            foreach (var sample in samples)
            {
                Add(sample);
            }
        }

        public void Clear()
        {
            Table.Rows.Clear();
        }
    }
}
