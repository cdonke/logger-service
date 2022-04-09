using System;
using System.IO;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogsHistSubscriber : SubscriberBase
    {
        public LogsHistSubscriber(IOptions<STLogOptions> options) : base(options)
        {
            if (!Directory.Exists(_options.LogsHist))
            {
                Directory.CreateDirectory(_options.LogsHist);
            }
        }

        protected override async void Publisher_LogEntityEnqueued(LogEntityEvent e)
        {
            var arquivo = Path.Combine(_options.LogsHist, $"{e.Aplicacao}.log");
            await EscreverLog(arquivo, e.Mensagem);
        }
    }
}
