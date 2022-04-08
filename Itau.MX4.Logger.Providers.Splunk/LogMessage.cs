using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Itau.MX4.Logger.Providers.Splunk.Extensions;

namespace Itau.MX4.Logger.Providers.Splunk
{
    public class LogMessage
    {
        private LogMessage() { }

        public static LogMessage Create(string requestId, string message) => Create(requestId, message, null);
        public static LogMessage Create<T>(string requestId, T obj) => Create(requestId, obj.Serialize());
        public static LogMessage Create(string requestId, string message, params object[] args) => new LogMessage
        {
            RequestId = requestId,
            Message = message,
            Args = args
        };

        public string RequestId { get; set; }
        public string Message { get; set; }
        public object[] Args { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
        public static implicit operator string(LogMessage o) => o.ToString();
    }
}
