using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Itau.MX4.Logger.Providers.Splunk.Debuggers
{
    internal class BasicDebugger : IDebugger
    {
        public BasicDebugger() { }

        public void Log(LogLevel logLevel, string loggerType, string message)
        {
            Debug.WriteLine($"[{logLevel.ToString()}] Splunk HEC {loggerType} Status: {message}");
        }

        public void LogDebug(string loggerType, string message)
            => Log(LogLevel.Debug, message, loggerType);

        public void LogInfo(string loggerType, string message)
            => Log(LogLevel.Information, message, loggerType);

        public void LogWarning(string loggerType, string message)
            => Log(LogLevel.Warning, message, loggerType);

        public void LogError(string loggerType, string message)
           => Log(LogLevel.Error, message, loggerType);

        public void LogTrace(string loggerType, string message)
         => Log(LogLevel.Trace, loggerType, message);
    }
}
