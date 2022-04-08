using System;
using System.IO;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogsSubscriber : SubscriberBase
    {
        public LogsSubscriber(IOptions<STLogOptions> Options) : base(Options) { }

        protected override async void Publisher_LogEntityEnqueued(LogEntityEvent e)
        {
            var arquivo = Path.Combine(_options.Logs, $"{e.Aplicacao}.log");
            await EscreverLog(arquivo, e.Mensagem);
        }
    }
}
