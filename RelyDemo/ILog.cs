using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RelyDemo
{
    public interface ILog
    {
        void Info(string message);
        void Warn(string message, Exception ex);
        void Warn(string message);
        void Debug(string message);
        void Debug(string message, Exception ex);
        void Error(string messag, Exception ex);
        void Error(string message);
    }
}
