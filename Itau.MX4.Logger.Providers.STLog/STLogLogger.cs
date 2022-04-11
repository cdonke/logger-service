using System;
using Itau.MX4.Logger.Providers.STLog.InternalModels;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog
{
    internal class STLogLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly STLogOptions _options;
        private readonly Interfaces.IPublisher _publisher;
        private readonly ILoggerFormatter<MessageEntity> _formatter;

        internal STLogLogger(string categoryName, STLogOptions options, ILoggerFormatter<MessageEntity> formatter, Interfaces.IPublisher publisher)
        {
            _categoryName = categoryName;
            _options = options;
            _publisher = publisher;
            _formatter = formatter;
        }

        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel)
        {
            return _options.IsEnabled;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (_formatter == null)
                throw new NullReferenceException($"{_formatter} está nulo");

            var mensagem = _formatter.FormatJson(string.Empty, logLevel, eventId, state, exception);

            if (!string.IsNullOrWhiteSpace(mensagem.Mensagem))
                _publisher.Postar(mensagem);
        }
    }
}
