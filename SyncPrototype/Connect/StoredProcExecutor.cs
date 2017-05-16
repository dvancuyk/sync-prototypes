
using Dapper;
using System.Collections.Generic;
using System.Data;

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
            var converted = new SampleTable();
            converted.AddRange(samples);

            return converted.Table;
        }
    }
}
