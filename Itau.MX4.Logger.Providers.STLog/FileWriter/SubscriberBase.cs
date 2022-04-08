using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal abstract class SubscriberBase
    {
        protected readonly STLogOptions _options;
        private readonly ConcurrentQueue<(string arquivo, string mensagem)> _deadLetterQueue;

        public SubscriberBase(IOptions<STLogOptions> Options)
        {
            _options = Options.Value;
            _deadLetterQueue = new ConcurrentQueue<(string arquivo, string mensagem)>();
        }

        public void StartListening()
        {
            Publisher.LogEntityEnqueued += Publisher_LogEntityEnqueued;
        }
        public void StopListening()
        {
            Publisher.LogEntityEnqueued -= Publisher_LogEntityEnqueued;
        }

        protected abstract void Publisher_LogEntityEnqueued(LogEntityEvent e);

        protected async Task EscreverLog(string arquivo, string mensagem)
        {
            try
            {
                using (var fs = new FileStream(arquivo, FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(fs))
                {
                    if (_deadLetterQueue.Count > 0)
                        await Retry(sw);

                    await sw.WriteLineAsync(mensagem);
                }
            }
            catch (Exception)
            {
                _deadLetterQueue.Enqueue((arquivo, mensagem));
            }
        }

        private async Task Retry(StreamWriter sw)
        {
            while (_deadLetterQueue.Count > 0)
            {
                if (_deadLetterQueue.TryDequeue(out (string arquivo, string mensagem) result))
                    await sw.WriteAsync(result.mensagem);
            }
        }
    }
}
