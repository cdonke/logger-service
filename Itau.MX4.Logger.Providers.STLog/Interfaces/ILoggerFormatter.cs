using Itau.MX4.Logger.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.STLog.Interfaces
{
    internal interface ILoggerFormatter
    {
        MessageEntity FormatText<TState>(TState state, Exception exception);
    }
}
