using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog
{
    public class STLogOptions
    {
        public bool IsEnabled { get; set; }
        public virtual string Logs { get; set; }
        public virtual string LogsHist { get; set; }
    }
}
