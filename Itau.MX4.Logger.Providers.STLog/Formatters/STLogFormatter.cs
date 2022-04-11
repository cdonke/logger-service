using System;
using System.Text.Json;
using Itau.MX4.Logger.Providers.STLog.InternalModels;
using Itau.MX4.Logger.Service.Domain.Interfaces;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog.Formatters
{
    internal class STLogFormatter : ILoggerFormatter<MessageEntity>
    {
        
        public string Format<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            try
            {
                var logEntity = state as LogEntity;
                return logEntity.BuildMessage();
            }
            catch (Exception)
            {
                return state.ToString();
            }
        }

        public MessageEntity FormatJson<T>(string categoryName, LogLevel logLevel, EventId eventId, T state, Exception exception)
        {
            LogEntity logEntity = null;
            MessageEntity result = new MessageEntity();
            try
            {
                logEntity = JsonSerializer.Deserialize<LogEntity>(state.ToString());
            }
            catch (JsonException)
            {
                return result;
            }

            result.Acao = logEntity.Acao;
            result.Aplicacao = logEntity.ApplicationName;
            result.Mensagem = Format<LogEntity>(null, logEntity.Level, 0, logEntity, null);


            return result;
        }


        
    }
}
