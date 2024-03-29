﻿using SyncPrototype.Client;
using SyncPrototype.Components;
using SyncPrototype.Components.Samples;
using SyncPrototype.Connect;
using System;
using System.Diagnostics;
using System.Linq;

namespace SyncPrototype.Tests
{
    public abstract class TestRun : IDisposable
    {
        private ILogger writer;
        public abstract string RunName { get; }        
        protected string AdditionalDescriptions { get; set; }
        public int Iterations { get; set; }
        
        protected TestRun(SmplRepository smpls, IRepository<Sample> samples, ILogger logger)
        {
            Iterations = 10;
            ClientRepository = smpls;
            ConnectRepository = samples;
            RepositoryName = samples.GetType().Name;
            this.writer = logger;
            ProcessorFactory = (client, connect) => new SampleProcessor(client, connect);
        }


        protected IRepository<Sample> ConnectRepository { get; }
        protected SmplRepository ClientRepository { get; }

        protected string RepositoryName { get; }

        public ILogger Writer
        {
            get
            {
                return writer;
            }
        }

        public Func<IRepository<Smpl>, IRepository<Sample>, SampleProcessor> ProcessorFactory { get; set; }

        public void Dispose()
        {

        }

        /// <summary>
        /// Performs any work which needs to be done prior to running all of the tests.
        /// </summary>
        protected virtual void Initialize()
        {

        }

        /// <summary>
        /// Hook which is called prior to each iteration of the test run being executed.
        /// </summary>
        protected virtual void PrepTest()
        {

        }

        public virtual void Run()
        {
            var timer = new Stopwatch();
            long[] times = new long[Iterations];
            Initialize();
            Writer.WriteLine("----------------------------------");
            Writer.WriteLine("Beginning {0} runs of the test: {1}", Iterations, RunName);

            for (var current = 0; current < Iterations; current++)
            {
                var processor = ProcessorFactory(ClientRepository, ConnectRepository);
                PrepTest();

                timer.Reset();
                timer.Start();

                processor.Process();

                timer.Stop();
                times[current] = timer.ElapsedMilliseconds;
                Writer.WriteLine("Iteration {0}: {1} ms", current + 1, timer.ElapsedMilliseconds);
            }

            Writer.WriteLine("Average for {0} iterations: {1}", Iterations, times.Average());
            Writer.WriteLine("Median for the {0} iterations: {1}", Iterations, times.Median());
        }

        private static double Average(long[] times)
        {
            
            return times.Sum() / (double)times.Length;
        }

    }
}
