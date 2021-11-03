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
        private readonly string name;
        private readonly AWConfigraution _aw;
        private readonly IFileOp op;
        
        public AWLogger(AWConfigraution aW,string na,IFileOp fileOp) {
            _aw = aW;
            name = na;
            op = fileOp;
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
                StringBuilder sb = new StringBuilder();
                sb = sb.Append(_aw.Filepath).Append(@"\").Append(DateTime.Now.ToString("d")).Append("_").Append(name).Append(".txt");
                var str = _aw.Filepath + @"\" + DateTime.Now.ToString("d") + "_" + name + ".txt";
                DirectoryInfo directoryInfo = new DirectoryInfo(_aw.Filepath);
                if (!directoryInfo.Exists)
                    return;
                if (!op.IsExists(str))
                    op.Create(str,name,logLevel,eventId,state,exception,formatter);
                else op.OpenWrite(str, name,logLevel, eventId, state, exception, formatter);
            }
        }
    }
}
