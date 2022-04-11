using System.Collections.Concurrent;
using Itau.MX4.Logger.Providers.STLog.InternalModels;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog
{
    [ProviderAlias("STLog")]
    internal class STLogProvider : ILoggerProvider
    {
        public readonly STLogOptions _options;
        private readonly Interfaces.IPublisher _publisher;
        private readonly ILoggerFormatter<MessageEntity> _formatter;
        private readonly ConcurrentDictionary<string, ILogger> _loggers;

        public STLogProvider(ILoggerFormatter<MessageEntity> formatter, Interfaces.IPublisher publisher, IOptions<STLogOptions> options)
        {
            _options = options.Value;
            _publisher = publisher;
            _formatter = formatter;
            _loggers = new ConcurrentDictionary<string, ILogger>();
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, _ => new STLogLogger(categoryName, _options, _formatter, _publisher));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
