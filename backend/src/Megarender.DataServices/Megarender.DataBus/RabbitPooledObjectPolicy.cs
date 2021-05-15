using System;
using Megarender.DataBus.Models;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Megarender.DataBus
{
    public class RabbitPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly RMQSettings _rabbitMqSettings;

        private readonly IConnection _connection;

        public RabbitPooledObjectPolicy(RMQSettings rmqSettings)
        {
            _rabbitMqSettings = rmqSettings ?? throw new NullReferenceException(nameof(RMQSettings));
            _connection = GetConnection();
        }


        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory {
                Uri = new Uri (_rabbitMqSettings.ConntectionString),
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds (10)
            };
            return factory.CreateConnection();
        }

        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
                return true;
            obj?.Dispose();
            return false;
        }
    }
}