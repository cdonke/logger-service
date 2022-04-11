using Itau.MX4.Logger.Providers.STLog.InternalModels;
using Itau.MX4.Logger.Service.Domain.Interfaces;
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

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerFormatter<MessageEntity>, Formatters.STLogFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<Interfaces.IPublisher, FileWriter.Publisher>());

            builder.Services.AddHostedService<Workers.FileWriterWorker>();

            builder.Services.AddSingleton<FileWriter.LogsHistSubscriber>();
            builder.Services.AddSingleton<FileWriter.LogsSubscriber>();
         
            LoggerProviderOptions.RegisterProviderOptions<STLogOptions, STLogProvider>(builder.Services);


            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, STLogProvider>());

            return builder;
        }
    }
}
