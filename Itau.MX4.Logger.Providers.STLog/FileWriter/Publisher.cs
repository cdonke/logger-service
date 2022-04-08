using System;
using Itau.MX4.Logger.Service.Models;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class Publisher: Interfaces.IPublisher
    {
        public static event LogEntityHandler LogEntityEnqueued;

        public void Postar(string mensagem)
        {
            OnLogEntityQueued(new LogEntityEvent(mensagem));
        }

        private void OnLogEntityQueued(LogEntityEvent logEntityEvent)
        {
            LogEntityEnqueued?.Invoke(logEntityEvent);
        }
    }
}
