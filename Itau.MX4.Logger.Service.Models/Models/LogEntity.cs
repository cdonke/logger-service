using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Service.Domain.Models
{
    public class LogEntity
    {
        private readonly static ConcurrentDictionary<string, string> _nomeAplicacao = new ConcurrentDictionary<string, string>();
        private readonly static CultureInfo _cultura = new System.Globalization.CultureInfo("pt-BR");

        private static JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            MaxDepth = 100,
            AllowTrailingCommas = true
        };

        public LogLevel Level { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public string ApplicationName { get; set; }
        public string ModuleName { get; set; }

        public Service.Models.Domain.Enums.Acao Acao { get; set; }


        public From From { get; set; } = new From();
        public Ambiente Ambiente { get; set; } = new Ambiente();
        public Excecao Excecao { get; set; } = new Excecao();



        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _jsonOptions);
        }


        [JsonIgnore]

        public string LogCode => $"{validaCodigo()}{tipoLog()}";

        public string BuildMessage()
        {
            switch (Acao)
            {
                default:
                case Service.Models.Domain.Enums.Acao.Log:
                    return MontarLog();


                case Service.Models.Domain.Enums.Acao.StartService:
                    return MontarStartService();


                case Service.Models.Domain.Enums.Acao.EndService:
                    return MontarStopService();
            }
        }

        private string MontarStopService()
            => $"{Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {formataNomeAplicacao()} 031I-Parando Servico";

        private string MontarStartService()
            => $"{Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {formataNomeAplicacao()} 031I-Iniciando Servico";

        private string MontarLog()
            => $"{Ambiente.ServerName} {DateTime.Now:dd/MM/yyyy HH:mm:ss}  {formataNomeAplicacao()} {LogCode}-{Message}";

        private string formataNomeAplicacao()
            => _nomeAplicacao.GetOrAdd(ApplicationName.ToLower(), (n) => _cultura.TextInfo.ToTitleCase(ApplicationName));


        private string tipoLog()
        {
            switch (Level)
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
        private string validaCodigo()
        {
            switch (Level)
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
