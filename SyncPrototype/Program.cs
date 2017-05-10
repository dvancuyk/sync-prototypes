using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Components.Samples;
using SyncPrototype.Connect;
using SyncPrototype.Db;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SyncPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var smpl = SmpleBuilder.Single(1);
            var mapper = new SampleConnectMapper();

            var sample = mapper.Convert(smpl);
            var first = new SampleIdentity(smpl);
            var second = SampleIdentity.Create(sample);

            if (first == second)
                Console.WriteLine("Success!");
        }

        private static TextWriter CreateWriter()
        {
            var directory = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "sync_prototype");

            var fileName = Path.Combine(directory,
                @"Run_" + DateTime.Now.ToString("HH_mm_ss") + ".txt");

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return new StreamWriter(fileName)
        }

        private static void OldMain()
        {

            const int iterations = 10;
            var factory = new SqlConnectionFactory();
            var smpls = new SmplRepository(factory);
            var samples = new SampleRepository(factory);

            //SeedNew(smpls);
            ushort delta = 50;
            Change(samples, delta);

            var timer = new Stopwatch();
            long[] times = new long[iterations];

            var fileName = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "sync_prototype",
                @"Run_" + DateTime.Now.ToString("HH_mm_ss") + ".txt");

            using (var writer = new System.IO.StreamWriter(fileName, false))
            {
                writer.WriteLine("Beginning {0} cycles with {1}% modified on Connect side", iterations, delta);

                for (var current = 0; current < iterations; current++)
                {
                    var processor = new SampleProcessor(smpls, samples);
                    //samples.Reset();
                    timer.Reset();
                    timer.Start();

                    processor.Process();

                    timer.Stop();
                    times[current] = timer.ElapsedMilliseconds;
                    writer.WriteLine("Iteration {0}: {1} ms", current + 1, timer.ElapsedMilliseconds);
                }

                writer.WriteLine("Average for {0} iterations: {1}", iterations, Average(times));
            }

        }

        private static double Average(long[] times)
        {
            return times.Sum() / (double)times.Length;
        }

        private static void Change(SampleRepository repository, ushort percentage)
        {
            var modifier = new PartialUpdates(repository, percentage);
        }

        private static void SeedNew(SmplRepository repository)
        {

            var seeder = new FirstTimeSyncSetup(repository);

            seeder.Seed();
        }
    }
}
