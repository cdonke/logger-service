using Itau.MX4.Logger.Providers.STLog.FileWriter;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Itau.MX4.Logger.Providers.STLog.Workers
{
    class FileWriterWorker : IHostedService
    {
        private readonly LogsSubscriber _logs;
        private readonly LogsHistSubscriber _logsHist;

        public FileWriterWorker(FileWriter.LogsSubscriber logs, FileWriter.LogsHistSubscriber logsHist)
        {
            _logs = logs;
            _logsHist = logsHist;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logs.StartListening();
            _logsHist.StartListening();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logs.StartListening();
            _logs.StopListening();

            return Task.CompletedTask;
        }
    }
}
