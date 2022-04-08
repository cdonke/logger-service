using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Itau.MX4.Logger.Providers.STLog
{
    public static class STLogExtensions
    {
        public static ILoggingBuilder AddSTLog(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<Formatters.STLogFormatter, Formatters.STLogFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<FileWriter.Publisher, FileWriter.Publisher>());

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<FileWriter.LogsSubscriber, FileWriter.LogsSubscriber>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<FileWriter.LogsHistSubscriber, FileWriter.LogsHistSubscriber>());
            


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, STLogProvider>());
            LoggerProviderOptions.RegisterProviderOptions<STLogOptions, STLogProvider>(builder.Services);

            return builder;
        }
    }
}
