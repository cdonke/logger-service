using System;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog
{
    internal class STLogLogger : ILogger
    {
        private readonly Interfaces.IPublisher _publisher;
        private readonly ILoggerFormatter<MessageEntity> _formatter;

        internal STLogLogger(ILoggerFormatter<MessageEntity> formatter, Interfaces.IPublisher publisher)
        {
            _publisher = publisher;
            _formatter = formatter;
        }

        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (_formatter == null)
                throw new NullReferenceException($"{_formatter} está nulo");

            var mensagem = _formatter.FormatJson(string.Empty, logLevel,eventId,state,exception);

            if (!string.IsNullOrWhiteSpace(mensagem.Mensagem))
                _publisher.Postar(mensagem);
        }
    }
}
