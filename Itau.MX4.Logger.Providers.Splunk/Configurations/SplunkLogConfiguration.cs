using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.Splunk.Configurations
{
    public class SplunkLogConfiguration
    {
        public SplunkLogConfiguration(IConfigurationSection config)
        {
            var levels = new Dictionary<string, LogLevel>();
            foreach (var key in config.GetSection("LogLevel").GetChildren())
            {
                if (Enum.TryParse<LogLevel>(key.Value, out LogLevel currentLogLevel))
                {
                    levels.Add(key.Key, currentLogLevel);
                }
            }

            LogLevelMapping = levels;
        }


        /// <summary>
        /// A dictionary containing the log level to console color mappings to be used while writing log entries to the console.
        /// </summary>
        public IReadOnlyDictionary<string, LogLevel> LogLevelMapping { get; }


        /// <summary>
        /// The min log level that will be written to the console, defaults to <see cref="LogLevel.Information"/>.
        /// </summary>
        public LogLevel MinLogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// If the logger is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
