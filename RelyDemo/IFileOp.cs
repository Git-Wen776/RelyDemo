using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
    public interface IFileOp
    {
        bool IsExists(string filepath);

        Task Create<TState>(string filepath, string name,LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);//创建并写入

        Task OpenWrite<TState>(string filepath, string name,LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);//打开并写如

        bool IsOver(string filepath);//判断是否超过指定大小
    }
}
