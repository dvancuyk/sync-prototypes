using SyncPrototype.Client;
using SyncPrototype.Connect;

namespace SyncPrototype.Tests
{
    public class TVPTestRun : TestRun
    {

        public TVPTestRun(SmplRepository smpls, SampleRepository samples, ILogger writer) 
            : base(smpls, samples, writer)
        {

        }

        public override string RunName => "TVP Test Run";


    }
}
