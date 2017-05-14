using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPrototype.Connect
{
    internal class StoredProcExecutor
    {
        private IDbConnection connection;

        public StoredProcExecutor(IDbConnection connection)
        {
            this.connection = connection;
        }

        public void Execute(string storedProc, IEnumerable<Sample> collection)
        {
            connection.Execute(storedProc, new { @samples = SampleType(collection) }, commandType: CommandType.StoredProcedure);
        }

        private static DataTable SampleType(IEnumerable<Sample> samples)
        {
            var table = new DataTable("SampleType");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Description", typeof(string));

            foreach (var sample in samples)
            {
                var row = table.Rows.Add(sample.Id, sample.Name, sample.Description);
            }

            return table;
        }
    }
}
