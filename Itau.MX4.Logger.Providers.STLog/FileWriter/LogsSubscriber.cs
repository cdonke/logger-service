using System;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogsSubscriber: SubscriberBase
    {
        public LogsSubscriber(IOptions<STLogOptions> Options):base(Options)
        {
        }

        protected override void Publisher_LogEntityEnqueued(LogEntityEvent e)
        {
            throw new NotImplementedException();
        }
    }
}
