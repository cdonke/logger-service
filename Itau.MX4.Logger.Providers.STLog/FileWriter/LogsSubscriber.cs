using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogsSubscriber : SubscriberBase
    {
        public LogsSubscriber(IOptions<STLogOptions> Options) : base(Options) { }

        protected override async void Publisher_LogEntityEnqueued(LogEntityEvent e)
        {
            var arquivo = Path.Combine(_options.Logs, $"{e.Aplicacao}.log");

            if (e.Acao == Service.Models.Enums.Acao.StartService)
            {
                int iTries = 0;
                do
                {
                    if (File.Exists(arquivo))
                    {
                        try
                        {
                            File.Delete(arquivo);
                            break;
                        }
                        catch (Exception)
                        {
                            await Task.Delay(250);
                        }
                    }
                } while (++iTries < 10);
            }

            await EscreverLog(arquivo, e.Mensagem);
        }
    }
}
