using System;
using System.Collections.Concurrent;
using System.IO;
using Itau.MX4.Logger.Providers.STLog.FileWriter;
using Itau.MX4.Logger.Providers.STLog.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog
{
    [ProviderAlias("STLog")]
    internal class STLogProvider : ILoggerProvider
    {
        public readonly STLogOptions _options;
        private readonly Interfaces.IPublisher _publisher;
        private readonly Interfaces.ILoggerFormatter _formatter;
        private readonly ConcurrentDictionary<string, ILogger> _loggers;

        public STLogProvider(Interfaces.ILoggerFormatter formatter, Interfaces.IPublisher publisher, IOptions<STLogOptions> options)
        {
            _options = options.Value;
            _publisher = publisher;
            _formatter = formatter;
            _loggers = new ConcurrentDictionary<string, ILogger>();
            

            //if (!Directory.Exists(_options.Caminho))
            //{
            //    Directory.CreateDirectory(_options.Caminho);
            //}
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, _ => new STLogLogger(_formatter, _publisher));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
