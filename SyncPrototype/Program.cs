using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using SyncPrototype.Tests;
using System;
using System.Collections.Generic;
using System.IO;

namespace SyncPrototype
{
    class Program
    {
        private static SqlConnectionFactory factory = new SqlConnectionFactory();
        private static SampleRepository connect = new SampleRepository(factory);
        private static SmplRepository client = new SmplRepository(factory);
        private static ILogger logger = CreateWriter("Merge vs Insert Delete Update");

        static void Main(string[] args)
        {
            try
            {
                foreach (var runner in Runs)
                {
                    runner.Run();
                    runner.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.WriteLine("Encountered exception: {0}{1}{2}", ex.Message,
                    Environment.NewLine, ex.StackTrace);
            }
            finally
            {

                logger.Dispose();
                client.Dispose();
                connect.Dispose();
                factory.Dispose();
            }


        }

        private static IEnumerable<TestRun> Runs
        {
            get
            {
                yield return new NewSyncTestRun(client, new TvpSampleRepository(connect), logger);
                yield return new NewSyncTestRun(client, connect, logger);
                var baseline = new ModifiedSyncTestRun(client, new TvpSampleRepository(connect), logger);
                yield return baseline;

                var variant = new ModifiedSyncTestRun(client, connect, logger);
                yield return variant;

                baseline.Inserts = variant.Inserts = 1;
                yield return baseline;
                yield return variant;

                baseline.Deletes = variant.Deletes = 4;
                yield return baseline;
                yield return variant;
                //yield return variant;
            }
        }
        private static CompositeWriter CreateWriter(string fileName)
        {
            var directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "sync_prototype");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return new CompositeWriter(Path.Combine(directory, fileName + DateTime.Now.ToShortTimeString().Replace(":", "") + ".txt"));
        }

        private static void Change(SampleRepository repository, ushort percentage)
        {
            var modifier = new ChangeSampleRepresentation(repository, percentage);
        }
    }
}
