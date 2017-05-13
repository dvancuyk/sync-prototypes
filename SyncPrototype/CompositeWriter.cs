using System;
using System.IO;

namespace SyncPrototype
{
    public interface ILogger : IDisposable
    {
        void WriteLine(string value, params object[] parameters);
    }

    public class CompositeWriter : ILogger
    {
        private StreamWriter writer;

        public CompositeWriter(string fileName)
        {
            FileName = fileName;
            Console.WriteLine("Output directed to {0}", fileName);
            writer = new StreamWriter(fileName);
        }

        public string FileName { get; }

        public void Dispose()
        {
            if(writer != null)
            {
                writer.Flush();
                writer.Dispose();
            }
        }

        public void WriteLine(string value, params object[] parameters)
        {
            Console.WriteLine(value, parameters);
            writer.WriteLine(value, parameters);
        }
    }
}
