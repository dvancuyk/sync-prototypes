using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Components.Samples;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using SyncPrototype.Tests;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SyncPrototype
{
    class Program
    {
        private static SqlConnectionFactory factory = new SqlConnectionFactory();
        private static SampleRepository connect = new SampleRepository(factory);
        private static SmplRepository client = new SmplRepository(factory);
        private static CompositeWriter writer = CreateWriter();

        static void Main(string[] args)
        {
            try
            {
                var runner = Current;
                runner.Run();
            }
            catch (Exception ex)
            {
                writer.Write("Encountered exception: {0}{1}{2}", ex.Message,
                    Environment.NewLine, ex.StackTrace);

                writer.Dispose();
                client.Dispose();
                connect.Dispose();
                factory.Dispose();
            }

        }

        private static TestRun Current
        {
            get
            {
                return new ModifiedSyncTestRun(client, connect, writer);
                //return new NewSyncTestRun(client, connect, writer);
            }
        }
        private static CompositeWriter CreateWriter()
        {
            var directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "sync_prototype");

            var fileName = Path.Combine(directory,
                @"Run_" + DateTime.Now.ToString("HH_mm_ss") + ".txt");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return new CompositeWriter(fileName);
        }

        private static void Change(SampleRepository repository, ushort percentage)
        {
            var modifier = new PartialUpdates(repository, percentage);
        }
    }
}
