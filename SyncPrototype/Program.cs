using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using SyncPrototype.Tests;
using System;
using System.IO;

namespace SyncPrototype
{
    class Program
    {
        private static SqlConnectionFactory factory = new SqlConnectionFactory();
        private static SampleRepository connect = new SampleRepository(factory);
        private static SmplRepository client = new SmplRepository(factory);
        private static ILogger logger = CreateWriter("TVP vs Single Saves");

        static void Main(string[] args)
        {
            try
            {
                foreach (var runner in Runs)
                {
                    runner.Run();
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

        private static TestRun[] Runs
        {
            get
            {
                return new TestRun[]
                {
                    new TVPInsertsTestRun(client, connect, logger),
                    new NewSyncTestRun(client, connect, logger),
                    new ModifiedSyncTestRun(client, connect, logger),
                    new TvpModifiedSyncTestRun(client, connect, logger)
            };
            }
        }
        private static CompositeWriter CreateWriter(string fileName)
        {
            var directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "sync_prototype");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return new CompositeWriter(Path.Combine(directory, fileName + ".txt"));
        }

        private static void Change(SampleRepository repository, ushort percentage)
        {
            var modifier = new PartialUpdates(repository, percentage);
        }
    }
}
