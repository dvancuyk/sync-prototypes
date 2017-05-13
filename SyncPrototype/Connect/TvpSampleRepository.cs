using System.Collections.Generic;
using SyncPrototype.Components;
using System.Data;
using Dapper;
using System;

namespace SyncPrototype.Connect
{
    public class TvpSampleRepository : IRepository<Sample>
    {
        private IRepository<Sample> repo;
        private List<Sample> samples = new List<Sample>();

        public TvpSampleRepository(IRepository<Sample> repo)
        {
            this.repo = repo;
        }

        public IConnectionFactory Factory => repo.Factory;

        public IEnumerable<Sample> All()
        {
            return repo.All();
        }

        public void Dispose()
        {
            repo.Dispose();
        }

        public void Save(Sample entity)
        {
            samples.Add(entity);
        }

        public void Save(IEnumerable<Sample> entities)
        {
            using (var connection = repo.Factory.Create())
            {
                connection.Execute("Samples_SaveCollection", new { samples = SampleType(entities) }, commandType: CommandType.StoredProcedure);
            }

            // see http://stackoverflow.com/questions/6232978/does-dapper-support-sql-2008-table-valued-parameters
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

        public void Finish()
        {
            Save(samples);
            samples.Clear();
        }

        public void Reset()
        {
            repo.Reset();
        }
    }
}
