using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
    public class Logger:ILog
    {
        private readonly ILogger _logger;
        public Logger(ILogger logger) {
            _logger = logger;
        }
        public void Debug(string message)
        {
            _logger.LogDebug(message);
        }

        public void Debug(string message, Exception ex)
        {
            _logger.LogDebug(message,ex);
        }

        public void Error(string messag, Exception ex)
        {
            _logger.LogError(messag,ex);
        }

        public void Error(string message)
        {
            _logger.LogError(message);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Warn(string message, Exception ex)
        {
            _logger.LogWarning(message,ex);
        }

        public void Warn(string message)
        {
            _logger.LogWarning(message);
        }
    }
}
