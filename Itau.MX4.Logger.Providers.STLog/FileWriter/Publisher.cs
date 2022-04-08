using System;
using Itau.MX4.Logger.Service.Models;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    public class Publisher
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
