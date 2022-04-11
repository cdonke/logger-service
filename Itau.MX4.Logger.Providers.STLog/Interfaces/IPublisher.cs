using Itau.MX4.Logger.Providers.STLog.InternalModels;

namespace Itau.MX4.Logger.Providers.STLog.Interfaces
{
    internal interface IPublisher
    {
        //static event LogEntityHandler LogEntityEnqueued;
        void Postar(MessageEntity mensagem);
    }
}
