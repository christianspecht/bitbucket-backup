using System;
using System.Collections.Generic;

namespace BitbucketBackup
{
    class CompositeLogger : ILogger, IDisposable
    {
        private readonly List<ILogger> loggers = new List<ILogger>();

        public void AddLogger(ILogger logger)
        {
            loggers.Add(logger);
        }

        public void WriteLine()
        {
            loggers.ForEach(x => x.WriteLine());
        }

        public void WriteLine(string msg, params object[] args)
        {
            loggers.ForEach(x => x.WriteLine(msg, args));
        }

        public void Flush()
        {
            loggers.ForEach(x => x.Flush());
        }

        public void Dispose()
        {
            Flush();
        }
    }
}
