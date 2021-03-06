using Itau.MX4.Logger.Providers.STLog.Interfaces;
using Itau.MX4.Logger.Providers.STLog.InternalModels;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class Publisher: IPublisher
    {
        public static event LogEntityHandler LogEntityEnqueued;

        public void Postar(MessageEntity mensagem)
        {
            OnLogEntityQueued(new LogEntityEvent(mensagem.Acao, mensagem.Mensagem, mensagem.Aplicacao));
        }

        private void OnLogEntityQueued(LogEntityEvent logEntityEvent)
        {
            LogEntityEnqueued?.Invoke(logEntityEvent);
        }
    }
}
