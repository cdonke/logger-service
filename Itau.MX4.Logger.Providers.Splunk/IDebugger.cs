using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.Splunk
{
    public interface IDebugger
    {
        void Log(LogLevel logLevel, string loggerType, string message);
        void LogDebug(string loggerType, string message);
        void LogInfo(string loggerType, string message);
        void LogWarning(string loggerType, string message);
        void LogError(string loggerType, string message);
        void LogTrace(string loggerType, string message);
    }
}
