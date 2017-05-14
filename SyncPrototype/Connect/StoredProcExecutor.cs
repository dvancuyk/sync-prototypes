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
            SampleTable table = new SampleTable();

            foreach (var sample in samples)
            {
                table.Add(sample);
            }

            return table.Table;
        }
    }
}
