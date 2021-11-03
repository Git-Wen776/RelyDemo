using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelyDemo
{
  public static class AWLoggerExtensions
    {

        public static ILoggingBuilder AddAWLogger(this ILoggingBuilder builder) {
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider,AWLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<AWConfigraution, AWLoggerProvider>(builder.Services);
            return builder;
        }

        public static ILoggingBuilder AddAWLogger(this ILoggingBuilder builder, Action<AWConfigraution> config) {
            builder.AddAWLogger();
            builder.Services.Configure(config);
            return builder;
        }
    }
}
