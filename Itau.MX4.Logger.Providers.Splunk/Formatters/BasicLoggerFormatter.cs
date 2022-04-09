using System;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.Splunk.Formatters
{
    public class BasicLoggerFormatter : ILoggerFormatter<SplunkJSONEntry>
    {
        public string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            return string.Format("{0}: [{1}] [{2}:{3}] {4} {5}",
                                 categoryName,
                                 logLevel.ToString(),
                                 eventId.Id,
                                 eventId.Name,
                                 state != null ? state.ToString() : string.Empty,
                                 exception != null ? exception.ToString() : string.Empty);
        }

        public SplunkJSONEntry FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            string eventText = Format(categoryName, logLevel, eventId, state, exception);
            return new SplunkJSONEntry(eventText);
        }
    }
}
