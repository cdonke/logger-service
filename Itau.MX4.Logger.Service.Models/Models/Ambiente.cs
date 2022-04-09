using System;
using System.Text.Json.Serialization;

namespace Itau.MX4.Logger.Service.Domain.Models
{
    public class Ambiente
    {
        [JsonIgnore]
        public string ServerName { get; } = Environment.MachineName;
        public string AssemblyName { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string Usuario { get; set; }
        public int ProcessId { get; set; }
        public int ThreadId { get; set; }
    }
}
