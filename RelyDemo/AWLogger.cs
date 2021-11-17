using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RelyDemo
{
    public class AWLogger : ILogger
    {
        private readonly AWConfigraution _aw;
        
        public AWLogger(AWConfigraution aW,string na,IFileOp fileOp) {
            _aw = aW;
        }
        public IDisposable BeginScope<TState>(TState state) => default;
        public bool IsEnabled(LogLevel logLevel)
        {
            return _aw.conColor.ContainsKey(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;
            if (_aw.eventId == 0 || _aw.eventId == eventId.Id)
            {
                var origioncolor =Console.ForegroundColor;
                Console.ForegroundColor = _aw.conColor[logLevel];
                Console.WriteLine($"{DateTime.Now}-{logLevel} - {eventId.Id} ");
                Console.ForegroundColor = origioncolor;
                Console.WriteLine($"{name}-{formatter(state, exception)}");
            }
        }
    }
}
