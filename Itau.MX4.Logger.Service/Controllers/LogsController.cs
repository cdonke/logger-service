using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Itau.MX4.Logger.Service.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly LogCollection _logCollection;

        public LogsController(LogCollection logCollection, ILogger<LogsController> logger)
        {
            _logCollection = logCollection;
        }

        [HttpPost("StartService/{servico}")]
        public void StartService([FromRoute] string servico, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            _logCollection.Enqueue(logEntity);
        }
        [HttpPost("StopService/{servico}")]
        public void StopService([FromRoute] string servico, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            _logCollection.Enqueue(logEntity);
        }

        [HttpPost("{servico}/{logLevel}")]
        public void Log([FromRoute] string servico, [FromRoute] LogLevel logLevel, [FromBody] LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            logEntity.Level = logLevel;

            _logCollection.Enqueue(logEntity);
        }
    }
}
