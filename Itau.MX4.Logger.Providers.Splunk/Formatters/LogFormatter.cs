using System;
using System.Text.Json;
using Itau.MX4.Logger.Providers.Splunk.Extensions;
using Itau.MX4.Logger.Domain.Interfaces;
using Itau.MX4.Logger.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.Splunk.Formatters
{



    public class LogFormatter : ILoggerFormatter<SplunkJSONEntry>
    {
        public string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            try
            {
                var logEntity = state as LogEntity;

                if (logEntity == null)
                    throw new Exception();

                var eventObj = new InternalModels.MessageEntity(logEntity);
                return JsonSerializer.Serialize(eventObj);
            }
            catch
            {
                return state.ToString();
            }
        }

        public SplunkJSONEntry FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            LogEntity logEntity = ParseState(state);
            var evt = Format(categoryName, logLevel, eventId, logEntity, exception);

            var epoch = logEntity.Date.ToUnixTimeSeconds();
            return new SplunkJSONEntry(
                evt,
                epoch,
                logEntity.Ambiente.ServerName,
                logEntity.ApplicationName
            );
        }


        private LogEntity ParseState<T>(T state)
        {
            LogEntity logEntity;
            try
            {
                logEntity = JsonSerializer.Deserialize<LogEntity>(state.ToString());
            }
            catch
            {
                logEntity = new LogEntity();
                logEntity.Message = state.ToString();
            }

            return logEntity;
        }
    }
}
