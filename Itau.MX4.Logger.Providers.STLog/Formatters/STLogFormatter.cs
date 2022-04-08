using System;
using System.Text.Json;
using Itau.MX4.Logger.Service.Models;

namespace Itau.MX4.Logger.Providers.STLog.Formatters
{
    public class STLogFormatter
    {
        public string FormatText<TState>(TState state, Exception exception)
        {
            try
            {
                var logEntity = JsonSerializer.Deserialize<LogEntity>(state.ToString());
                var mensagem = $"{logEntity.Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {logEntity.ApplicationName} {validaCodigo()}{tipoLog(logEntity)}-{logEntity.Message}";

                return mensagem;
            }
            catch (JsonException)
            {
                return string.Empty;
            }
        }

        private object tipoLog(LogEntity logEntity)
        {
            switch (logEntity.Level)
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

        private object validaCodigo()
        {
            throw new NotImplementedException();
        }
    }
}
