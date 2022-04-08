using System;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal abstract class SubscriberBase
    {
        protected readonly STLogOptions _options;

        public SubscriberBase(IOptions<STLogOptions> Options)
        {
            _options = Options.Value;
            Publisher.LogEntityEnqueued += Publisher_LogEntityEnqueued;
        }

        protected abstract void Publisher_LogEntityEnqueued(LogEntityEvent e);
    }
}
