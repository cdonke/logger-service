using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Itau.MX4.Logger.Service.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Itau.MX4.Logger.Service
{
    public class LogCollection
    {
        private readonly ILogger<LogCollection> _logger;
        private readonly BlockingCollection<LogEntity> _blockingCollection;

        public LogCollection(ILogger<LogCollection> logger)
        {
            _logger = logger;

            _blockingCollection = new BlockingCollection<LogEntity>();

            Initialize();
        }

        private void Initialize()
        {
            Task.Run(() =>
            {
                while (!_blockingCollection.IsCompleted)
                {
                    LogEntity message = null;
                    try
                    {
                        message = _blockingCollection.Take();
                    }
                    catch (InvalidOperationException) { }

                    if (message != null)
                    {
                        var json = JsonSerializer.Serialize(message);
                        _logger.Log(message.Level, json);
                    }
                }
            });
        }

        public void Enqueue(LogEntity message)
        {
            _blockingCollection.Add(message);
        }

        public void Enqueue(string message)
        {
            var obj = JsonSerializer.Deserialize<LogEntity>(message);
            _blockingCollection.Add(obj);
        }
    }
}
