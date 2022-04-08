using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.Splunk.Debuggers
{
    class NoDebugger : IDebugger
    {
        public void Log(LogLevel logLevel, string loggerType, string message)
        {
        }

        public void LogDebug(string loggerType, string message)
        {
        }

        public void LogError(string loggerType, string message)
        {
        }

        public void LogInfo(string loggerType, string message)
        {
        }

        public void LogTrace(string loggerType, string message)
        {
        }

        public void LogWarning(string loggerType, string message)
        {
        }
    }
}
