using System;
using System.Text.Json;
using Itau.MX4.Logger.Service.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Providers.STLog.Formatters
{
    internal class STLogFormatter :Interfaces.ILoggerFormatter
    {
        public string FormatText<TState>(TState state, Exception exception)
        {
            try
            {
                var logEntity = JsonSerializer.Deserialize<LogEntity>(state.ToString());
                var mensagem = $"{logEntity.Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {logEntity.ApplicationName} {validaCodigo(logEntity.Level)}{tipoLog(logEntity.Level)}-{logEntity.Message}";

                return mensagem;
            }
            catch (JsonException)
            {
                return string.Empty;
            }
        }

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

/*
 case LevelType.FatalError:
                    ((ClsBatch)stlog).GravarLog("998", ClsBatch.enum_TipoLogBatch.tipoLog_erro, log.ErrorMessage);
                    break;
                case LevelType.Error:
                    ((ClsBatch)stlog).GravarLog("900", ClsBatch.enum_TipoLogBatch.tipoLog_erro, log.ErrorMessage);
                    break;
                case LevelType.Warning:
                    ((ClsBatch)stlog).GravarLog("401", ClsBatch.enum_TipoLogBatch.tipoLog_aviso, log.Message);
                    break;
                case LevelType.Info:
                    ((ClsBatch)stlog).GravarLog("001", ClsBatch.enum_TipoLogBatch.tipoLog_info, log.Message);
                    break;
                case LevelType.Trace:
                    ((ClsBatch)stlog).GravarLog("002", ClsBatch.enum_TipoLogBatch.tipoLog_info, log.Message);
                    break;
                case LevelType.Verbose:
                    ((ClsBatch)stlog).GravarLog("003", ClsBatch.enum_TipoLogBatch.tipoLog_info, log.Message);
                    break;
                case LevelType.StartService:
                    ((ClsBatch)stlog).IniciarLog(null, log.ApplicationName);
                    isinitialize = true;
                    break;
                case LevelType.StopService:
                    ((ClsBatch)stlog).FinalizarLog(ClsBatch.enum_TipoFimLogBatch.tipoFimLog_normal);
                    stlog = null;
                    isinitialize = false;
                    break;
                case LevelType.StopServiceWithError:
                    ((ClsBatch)stlog).FinalizarLog(ClsBatch.enum_TipoFimLogBatch.tipoFimLog_erro);
                    break;
                case LevelType.Alive:
                case LevelType.Security:
 */
