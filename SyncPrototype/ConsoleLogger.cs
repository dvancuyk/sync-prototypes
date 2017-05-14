using System;

namespace SyncPrototype
{
    public class ConsoleLogger : ILogger
    {
        public void Dispose()
        {

        }

        public void WriteLine(string value, params object[] parameters)
        {
            Console.WriteLine(value, parameters);
        }
    }
}
