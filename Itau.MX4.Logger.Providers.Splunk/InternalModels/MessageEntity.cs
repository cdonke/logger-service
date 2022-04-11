using System;
using Itau.MX4.Logger.Domain.Models;
using Itau.MX4.Logger.Service.Models.Domain.Enums;

namespace Itau.MX4.Logger.Providers.Splunk.InternalModels
{
    class MessageEntity
    {
        public MessageEntity(LogEntity logEntity)
        {
            LogLevel = logEntity.Level.ToString();
            Mensagem = logEntity.Message;
            Codigo = logEntity.LogCode;

            ParseAcao(logEntity.Acao);
        }

        public string LogLevel { get; private set; }
        public string Mensagem { get; private set; }
        public string Codigo { get; private set; }


        private void ParseAcao(Acao acao)
        {
            switch (acao)
            {
                case Service.Models.Domain.Enums.Acao.StartService:
                    Mensagem = "Servico Inicializando";
                    break;

                case Service.Models.Domain.Enums.Acao.EndService:
                    Mensagem = "Servico Encerrando";
                    break;
            }
        }
    }
}
