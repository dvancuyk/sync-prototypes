using SyncPrototype.Client;
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
        }

        internal DataTable Table { get; }

        public void Add(Sample sample, bool remove = false)
        {
            Table.Rows.Add(sample.Id, sample.Name, sample.Description, remove);
        }

        public void Add(Smpl sample, bool remove = false)
        {
            Table.Rows.Add(0, sample.Name, sample.Description, remove);
        }

        public void Clear()
        {
            Table.Rows.Clear();
        }
    }
}
