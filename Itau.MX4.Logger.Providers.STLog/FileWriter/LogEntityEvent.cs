using System;
using Itau.MX4.Logger.Service.Models;

namespace Itau.MX4.Logger.Providers.STLog.FileWriter
{
    internal class LogEntityEvent : EventArgs
    {
        public LogEntityEvent(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; private set; }
    }
}
