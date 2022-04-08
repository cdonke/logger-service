using System;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Service.Models
{
    public class LogEntity
    {
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

        public Enum.Acao Acao { get; set; }


        public From From { get; set; } = new From();
        public Ambiente Ambiente { get; set; } = new Ambiente();
        public Excecao Excecao { get; set; } = new Excecao();



        public override string ToString()
        {
            return JsonSerializer.Serialize(this, _jsonOptions);
        }
    }
}
