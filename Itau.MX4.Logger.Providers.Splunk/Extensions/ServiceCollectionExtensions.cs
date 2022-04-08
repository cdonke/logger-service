using Itau.MX4.Logger.Providers.Splunk.Configurations;
using Itau.MX4.Logger.Providers.Splunk.Formatters;
using Itau.MX4.Logger.Providers.Splunk.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.Splunk
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adiciona o Logger do Splunk para Json
        /// </summary>
        /// <param name="builder">Logger builder</param>
        /// <param name="configuration">Seção raíz de onde estará o elemento "Splunk"</param>
        /// <param name="formatter">Formatter customizado</param>
        /// <returns></returns>
        public static ILoggingBuilder AddSplunkJsonLogger(this ILoggingBuilder builder, ILoggerFormatter formatter = null)
        {
            ConfigureLogger(builder, formatter);

            LoggerProviderOptions.RegisterProviderOptions<SplunkLoggerConfiguration, SplunkHECJsonLoggerProvider>(builder.Services);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkHECJsonLoggerProvider>());
            return builder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder">Logger builder</param>
        /// <param name="configuration">Seção raíz de onde estará o elemento "Splunk"</param>
        /// <param name="formatter">Formatter customizado</param>
        /// <returns></returns>
        public static ILoggingBuilder AddSplunkRawLogger(this ILoggingBuilder builder, IConfiguration configuration,
            ILoggerFormatter formatter = null)
        {
            ConfigureLogger(builder, formatter);

            LoggerProviderOptions.RegisterProviderOptions<SplunkLoggerConfiguration, SplunkHECRawLoggerProvider>(builder.Services);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkHECRawLoggerProvider>());
            return builder;
        }

        /// <summary>
        /// Add <see cref="T:Splunk.Providers.SplunkTcpLoggerProvider"/> as provider to logger factory.
        /// </summary>
        /// <param name="loggerFactory">Logger factory.</param>
        /// <param name="configuration">Seção raíz de onde estará o elemento "Splunk"</param>
        /// <param name="formatter">Formatter customizado</param>
        public static ILoggingBuilder AddTcpSplunkLogger(this ILoggingBuilder builder, IConfiguration configuration,
            ILoggerFormatter formatter = null)
        {
            ConfigureLogger(builder, formatter);

            LoggerProviderOptions.RegisterProviderOptions<SplunkLoggerConfiguration, SplunkTcpLoggerProvider>(builder.Services);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkTcpLoggerProvider>());
            return builder;
        }

        /// <summary>
        /// Add <see cref="T:Splunk.Providers.SplunkUdpLoggerProvider"/> as provider to logger factory.
        /// </summary>
        /// <param name="loggerFactory">Logger builder</param>
        /// <param name="configuration">Seção raíz de onde estará o elemento "Splunk"</param>
        /// <param name="formatter">Formatter customizado</param>
        public static ILoggingBuilder AddUdpSplunkLogger(this ILoggingBuilder builder, IConfiguration configuration,
            ILoggerFormatter formatter = null)
        {
            ConfigureLogger(builder, formatter);

            LoggerProviderOptions.RegisterProviderOptions<SplunkLoggerConfiguration, SplunkUdpLoggerProvider>(builder.Services);

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, SplunkUdpLoggerProvider>());
            return builder;
        }



        private static void ConfigureLogger(ILoggingBuilder builder, ILoggerFormatter formatter)
        {
            builder.AddConfiguration();

            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            builder.Services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

            builder.Services.TryAdd(ServiceDescriptor.Singleton((sp) => formatter ?? new JsonLoggerFormatter()));

        }
    }
}
