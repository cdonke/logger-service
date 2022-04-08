using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog
{
    public static class STLogExtensions
    {
        public static ILoggingBuilder AddSTLog(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<Interfaces.ILoggerFormatter, Formatters.STLogFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<Interfaces.IPublisher, FileWriter.Publisher>());

            builder.Services.AddSingleton<FileWriter.LogsHistSubscriber>();
            builder.Services.AddSingleton<FileWriter.LogsSubscriber>();
         
            LoggerProviderOptions.RegisterProviderOptions<STLogOptions, STLogOptions>(builder.Services);


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, STLogProvider>());

            return builder;
        }
    }
}
