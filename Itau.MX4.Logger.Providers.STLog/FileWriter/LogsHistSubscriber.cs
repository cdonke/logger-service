using System;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogsHistSubscriber : SubscriberBase
    {
        public LogsHistSubscriber(IOptions<STLogOptions> options) : base(options)
        {
        }

        protected override void Publisher_LogEntityEnqueued(LogEntityEvent e)
        {
        }
    }
}
