using System;
using System.Net.Http;
using Itau.MX4.Logger.Providers.Splunk.Configurations;
using Itau.MX4.Logger.Providers.Splunk.Formatters;
using Itau.MX4.Logger.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.Splunk.Loggers
{
    /// <summary>
    /// Class used to send log to splunk as JSON via HEC.
    /// </summary>
    public class HECJsonLogger : HECBaseLogger, ILogger
    {
        private readonly SplunkLogConfiguration _loggingConfiguration;
        private readonly string _categoryName;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Splunk.Loggers.HECJsonLogger"/> class.
        /// </summary>
        /// <param name="categoryName">Category name.</param>
        /// <param name="httpClient">Http client.</param>
        /// <param name="batchManager">Batch manager.</param>
        /// <param name="loggerFormatter">Formatter instance.</param>
        public HECJsonLogger(string categoryName, HttpClient httpClient, BatchManager batchManager, ILoggerFormatter<SplunkJSONEntry> loggerFormatter)
            : this(categoryName, httpClient, batchManager, loggerFormatter, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Splunk.Loggers.HECJsonLogger"/> class.
        /// </summary>
        /// <param name="categoryName">Category name.</param>
        /// <param name="httpClient">Http client.</param>
        /// <param name="batchManager">Batch manager.</param>
        /// <param name="loggerFormatter">Formatter instance.</param>
        public HECJsonLogger(string categoryName, HttpClient httpClient, BatchManager batchManager, ILoggerFormatter<SplunkJSONEntry> loggerFormatter, SplunkLogConfiguration loggingConfiguration)
            : base(categoryName, httpClient, batchManager, loggerFormatter)
        {
            _loggingConfiguration = loggingConfiguration;
            _categoryName = categoryName;
        }

        public override bool IsEnabled(LogLevel logLevel)
        {
            if (_loggingConfiguration == null)
                return base.IsEnabled(logLevel);

            if (!_loggingConfiguration.IsEnabled)
                return false;

            if (!_loggingConfiguration.LogLevelMapping.TryGetValue(_categoryName, out LogLevel ll))
            {
                if (!_loggingConfiguration.LogLevelMapping.TryGetValue("Default", out ll))
                {
                    ll = _loggingConfiguration.MinLogLevel;
                }
            }

            return logLevel >= ll;
        }

        /// <summary>
        /// Method used to create log.
        /// </summary>
        /// <returns>The log.</returns>
        /// <param name="logLevel">Log level.</param>
        /// <param name="eventId">Log event identifier.</param>
        /// <param name="state">Log object state.</param>
        /// <param name="exception">Log Exception.</param>
        /// <param name="formatter">Log text formatter function.</param>
        public override void Log<T>(LogLevel logLevel, EventId eventId, T state, Exception exception, Func<T, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;


            SplunkJSONEntry formatedMessage = null;
            if (loggerFormatter != null)
                formatedMessage = loggerFormatter.FormatJson(categoryName, logLevel, eventId, state, exception);
            else if (formatter != null)
                formatedMessage = new SplunkJSONEntry(formatter(state, exception));

            batchManager.Add(formatedMessage);

            //batchManager.Add(JObject.FromObject(formatedMessage));
        }
    }
}