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
            DeclareSchema();
        }


        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory {
                Uri = new Uri (_rabbitMqSettings.ConnectionString),
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds (10),
              //  DispatchConsumersAsync = true
            };
            return factory.CreateConnection();
        }

        public void DeclareSchema()
        {
            var channel = Create();
            if(_rabbitMqSettings.Queues is not null)
            {
                foreach (var queue in _rabbitMqSettings.Queues)
                {
                    channel.QueueDeclare(queue.Name,queue.Durable,queue.Exclusive,queue.AutoDelete);
                }
            }
            if(_rabbitMqSettings.Exchanges is not null)
            {
                foreach (var exchange in _rabbitMqSettings.Exchanges)
                {
                    channel.ExchangeDeclare(exchange.Name, exchange.Type.ToString().ToLowerInvariant(), exchange.Durable, exchange.AutoDelete, null);
                }
            }
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