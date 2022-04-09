namespace Itau.MX4.Logger.Service.Domain.Models
{
    public class Excecao
    {
        public int? Code { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Trace { get; set; }
    }
}
