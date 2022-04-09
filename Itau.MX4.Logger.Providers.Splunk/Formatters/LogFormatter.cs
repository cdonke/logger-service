using System;
using System.Text.Json;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.Splunk.Formatters
{
    public class LogFormatter: ILoggerFormatter<SplunkJSONEntry>
    {
        public string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            try
            {
                var logEntity = state as LogEntity;
                return $"{logEntity.LogCode}-{logEntity.Message}";
            }
            catch (Exception)
            {
                return state.ToString();
            }
        }

        public SplunkJSONEntry FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            LogEntity logEntity = ParseState(state);
            var evt = Format<LogEntity>(categoryName, logLevel, eventId, logEntity, exception);

            var epoch = (ulong)(new DateTimeOffset(logEntity.Date)).ToUnixTimeSeconds();
            return new SplunkJSONEntry(evt, epoch, logEntity.Ambiente.ServerName,
                logEntity.ApplicationName);
        }


        private LogEntity ParseState<T>(T state)
        {
            LogEntity logEntity;
            try
            {
                logEntity = JsonSerializer.Deserialize<LogEntity>(state.ToString());
            }
            catch (Exception ex)
            {
                logEntity = new LogEntity();
                logEntity.Message = state.ToString();
            }

            return logEntity;
        }
    }
}
