using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog
{
    public class STLogOptions
    {
        public STLogOptions(IConfigurationSection config)
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


        public bool IsEnabled { get; set; }
        public virtual string Logs { get; set; }
        public virtual string LogsHist { get; set; }



        /// <summary>
        /// A dictionary containing the log level to console color mappings to be used while writing log entries to the console.
        /// </summary>
        public IReadOnlyDictionary<string, LogLevel> LogLevelMapping { get; }


        /// <summary>
        /// The min log level that will be written to the console, defaults to <see cref="LogLevel.Information"/>.
        /// </summary>
        public LogLevel MinLogLevel { get; set; } = LogLevel.Information;
    }
}
