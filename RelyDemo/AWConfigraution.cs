using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
    public class AWConfigraution
    {
        public Dictionary<LogLevel, ConsoleColor> conColor { get; set; } = new() {
            [LogLevel.Information] = ConsoleColor.Green,
            [LogLevel.Error]=ConsoleColor.Red,
            [LogLevel.Debug]=ConsoleColor.Magenta,
            [LogLevel.Warning]=ConsoleColor.Yellow
        };
        public EventId eventId{get;set;}
        public string Filepath { get; set; } = @"E:\vs 2019\RelyDemo\RelyDemo\bin\Debug\net5.0";//日志文件保存地址
        private int filesize;
        public int FileSize
        {
            get { return filesize; }
            set { if (value >1024000)
                    filesize = 1024000;
                filesize = value;
            }
        }
    }
}
