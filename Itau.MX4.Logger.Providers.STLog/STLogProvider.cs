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
    public class STLogProvider : ILoggerProvider
    {
        public readonly STLogOptions _options;
        private readonly Publisher _publisher;
        private readonly STLogFormatter _formatter;
        private readonly ConcurrentDictionary<string, ILogger> _loggers;

        public STLogProvider(Formatters.STLogFormatter formatter, FileWriter.Publisher publisher, IOptions<STLogOptions> options)
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
