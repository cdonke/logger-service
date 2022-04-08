using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;
using Itau.MX4.Logger.Providers.Splunk.Extensions;

namespace Itau.MX4.Logger.Providers.Splunk.Formatters
{
    public class JsonLoggerFormatter : ILoggerFormatter
    {
        public string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            throw new NotImplementedException();
        }

        public SplunkJSONEntry FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {  
            var eventObj = new LoggerMessage
            {
                categoryName = categoryName,
                logLevel = logLevel.ToString(),
                exception = exception?.ToString()
            };

            if (state != null)
            {
                try
                {
                    var logMessage = state.ToString().Deserialize<LogMessage>();
                    eventObj.RequestId = logMessage.RequestId;
                    if (logMessage.Args != null)
                        eventObj.message = string.Format(logMessage.Message, logMessage.Args);
                    else
                        eventObj.message = logMessage.Message;
                }
                catch (JsonException)
                {
                    try
                    {
                        eventObj.message = JsonSerializer.Deserialize<string>(state.ToString());
                    }
                    catch (Exception)
                    {
                        eventObj.message = state.ToString();
                    }
                }
            }

            return new SplunkJSONEntry(eventObj.Serialize());
        }

        private class LoggerMessage
        {
            public string RequestId { get; set; }
            public string categoryName { get; set; }
            public string logLevel { get; set; }
            public object message { get; set; }
            public string exception { get; set; }
        }
    }
}
