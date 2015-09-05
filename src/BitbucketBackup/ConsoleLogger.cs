using System;

namespace BitbucketBackup
{
    class ConsoleLogger : ILogger
    {
        public void WriteLine()
        {
            WriteLine(Environment.NewLine);
        }

        public void WriteLine(string msg, params object[] args)
        {
            Console.WriteLine(msg, args);
        }

        public void Flush()
        {
        }
    }
}