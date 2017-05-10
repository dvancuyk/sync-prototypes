using System;
using System.IO;

namespace SyncPrototype
{
    public interface ILogger
    {
        void WriteLine(string value, params object[] parameters);
    }

    public class CompositeWriter : StreamWriter, ILogger
    {
        public CompositeWriter(string fileName) : base(fileName)
        {
            Console.WriteLine("Output directed to {0}", fileName);
        }

        public override void WriteLine(string value, params object[] parameters)
        {
            Console.WriteLine(value, parameters);
            base.Write(value);
        }
    }
}
