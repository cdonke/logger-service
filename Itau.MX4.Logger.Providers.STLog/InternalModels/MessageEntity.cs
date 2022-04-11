using System;
using System.Collections.Generic;
using System.Text;

namespace Itau.MX4.Logger.Providers.STLog.InternalModels
{
    public class MessageEntity
    {
        public string Aplicacao { get; set; }
        public string Mensagem { get; set; }
        public Service.Models.Domain.Enums.Acao Acao { get; set; }
    }
}
