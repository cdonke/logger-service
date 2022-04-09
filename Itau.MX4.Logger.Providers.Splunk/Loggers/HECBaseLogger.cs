using System.Net.Http;
using Itau.MX4.Logger.Providers.Splunk.Formatters;
using Itau.MX4.Logger.Service.Domain.Interfaces;

namespace Itau.MX4.Logger.Providers.Splunk.Loggers
{
    /// <summary>
    /// Define a base HEC logger class.
    /// </summary>
    public abstract class HECBaseLogger : BaseLogger
    {
        protected readonly BatchManager batchManager;

        readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Splunk.Loggers.HECBaseLogger"/> class.
        /// </summary>
        /// <param name="categoryName">Category name.</param>
        /// <param name="httpClient">Http client.</param>
        /// <param name="batchManager">Batch manager.</param>
        /// <param name="loggerFormatter">Formatter instance.</param>
        public HECBaseLogger(string categoryName, HttpClient httpClient, BatchManager batchManager, ILoggerFormatter<SplunkJSONEntry> loggerFormatter)
            : base(categoryName, loggerFormatter)
        {
            this.httpClient = httpClient;
            this.batchManager = batchManager;
        }
    }
}