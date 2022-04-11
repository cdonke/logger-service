using System;
using Itau.MX4.Logger.Providers.STLog.InternalModels;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogEntityEvent : EventArgs
    {
        public LogEntityEvent(MessageEntity mensagem) : this(mensagem.Acao, mensagem.Mensagem, mensagem.Aplicacao) { }
        public LogEntityEvent(Service.Models.Domain.Enums.Acao acao, string mensagem, string aplicacao)
        {
            Acao = acao;
            Mensagem = mensagem;
            Aplicacao = aplicacao;
        }

        public string Mensagem { get; private set; }
        public string Aplicacao { get; internal set; }
        public Service.Models.Domain.Enums.Acao Acao { get; internal set; }
    }
}
