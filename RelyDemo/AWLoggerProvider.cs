using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
    public class AWLoggerProvider : ILoggerProvider
    {
        private readonly AWConfigraution _config;
        private readonly IFileOp op;
        private ConcurrentDictionary<string,AWLogger> awLoggers = new ConcurrentDictionary<string, AWLogger>();

        public AWLoggerProvider(AWConfigraution config,IFileOp fileOp) {
            _config = config;
            op = fileOp;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return awLoggers.GetOrAdd(categoryName, logger => new AWLogger(_config, categoryName,op));
        }

        public void Dispose()
        {
            awLoggers.Clear();
        }
    }
}
