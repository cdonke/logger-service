using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Itau.MX4.Logger.Providers.STLog.Interfaces;
using Itau.MX4.Logger.Service.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog.Formatters
{
    internal class STLogFormatter : Interfaces.ILoggerFormatter
    {
        private readonly static ConcurrentDictionary<string, string> _nomeAplicacao = new ConcurrentDictionary<string, string>();
        private readonly static CultureInfo _cultura = new System.Globalization.CultureInfo("pt-BR");

        public MessageEntity FormatText<TState>(TState state, Exception exception)
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

            result.Aplicacao = logEntity.ApplicationName;
            result.Mensagem = $"{logEntity.Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {formataNomeAplicacao(logEntity.ApplicationName)} {validaCodigo(logEntity.Level)}{tipoLog(logEntity.Level)}-{logEntity.Message}";

            return result;
        }

        private string formataNomeAplicacao(string nomeAplicacao)
            => _nomeAplicacao.GetOrAdd(nomeAplicacao.ToLower(), (n) => _cultura.TextInfo.ToTitleCase(nomeAplicacao));


        private string tipoLog(LogLevel level)
        {
            switch (level)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return "I";


                case Microsoft.Extensions.Logging.LogLevel.Error:
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return "E";

                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return "W";

                default:
                    return "I";
            }
        }
        private string validaCodigo(LogLevel level)
        {
            switch (level)
            {
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                    return "002";

                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return "003";

                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return "001";

                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return "401";

                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return "900";

                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return "998";

                case Microsoft.Extensions.Logging.LogLevel.None:
                default:
                    return "000";
            }
        }
    }
}
