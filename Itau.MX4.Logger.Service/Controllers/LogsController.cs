using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Itau.MX4.Logger.Service.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly LogCollection _observable;

        public LogsController(LogCollection observable, ILogger<LogsController> logger)
        {
            _observable = observable;
        }


        // POST api/values
        [HttpPost("{servico}/{logLevel}")]
        public void Post([FromRoute]string servico, [FromRoute]LogLevel logLevel, [FromBody]Models.LogEntity logEntity)
        {
            logEntity.ApplicationName = servico;
            logEntity.Level = logLevel;

            _observable.Enqueue(logEntity);
        }
    }
}
