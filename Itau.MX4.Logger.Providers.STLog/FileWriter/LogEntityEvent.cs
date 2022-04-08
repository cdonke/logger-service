using System;
using Itau.MX4.Logger.Service.Models;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogEntityEvent : EventArgs
    {
        public LogEntityEvent(MessageEntity mensagem) : this(mensagem.Mensagem, mensagem.Aplicacao) { }
        public LogEntityEvent(string mensagem, string aplicacao)
        {
            Mensagem = mensagem;
            Aplicacao = aplicacao;
        }

        public string Mensagem { get; private set; }
        public string Aplicacao { get; internal set; }
    }
}
