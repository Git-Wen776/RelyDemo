using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;

namespace RelyDemo
{
    public class FileOp : IFileOp
    {
        public async Task Create<TState>(string filepath,string name,LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            await sw.WriteLineAsync($"程序{name}-{logLevel}");
            await sw.WriteLineAsync($"{formatter(state, exception)}");
            sw.Close();
            fs.Close();
        }
        public bool IsExists(string filepath)
        {
            return new FileInfo(filepath).Exists;
        }
        public bool IsOver(string filepath)
        {
            return new FileInfo(filepath).Length > 1024000;
        }
        public async Task OpenWrite<TState>(string filepath, string name,LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            FileStream fs = new FileStream(filepath,FileMode.Open,FileAccess.Write,FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            await sw.WriteLineAsync($"程序{name}-{logLevel}");
            await sw.WriteLineAsync($"{formatter(state, exception)}");
            sw.Close();
            fs.Close();
        }
    }
}
