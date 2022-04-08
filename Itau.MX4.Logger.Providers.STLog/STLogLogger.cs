using System;
using System.IO;
using System.Text.Json;
using Itau.MX4.Logger.Providers.STLog.FileWriter;
using Itau.MX4.Logger.Providers.STLog.Formatters;
using Itau.MX4.Logger.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog
{
    public class STLogLogger : ILogger
    {
        private readonly Publisher _publisher;
        private readonly STLogFormatter _formatter;

        public STLogLogger(Formatters.STLogFormatter formatter, FileWriter.Publisher publisher)
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


            var mensagem = string.Empty;
            if (_formatter != null)
                mensagem = _formatter.FormatText(state, exception);
            else
                mensagem = formatter(state, exception);

            if (!string.IsNullOrWhiteSpace(mensagem))
                _publisher.Postar(mensagem);
        }
    }
}
