using System;
using System.Text;
using Megarender.DataBus.Models;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Megarender.DataBus
{
    public class RMQMessageProducerService: IMessageProducerService
    {
        private readonly DefaultObjectPool<IModel> _objectPool;
        private readonly RMQSettings _rmqSettings;

        public RMQMessageProducerService(IPooledObjectPolicy<IModel> objectPolicy, RMQSettings rmqSettings)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);  ;
            _rmqSettings = rmqSettings;
        }

        public void Enqueue<T>(Envelope<T> envelope, string routingKey) where T : IMessage
        {
            if (envelope == null)
                return;
            var channel = _objectPool.Get();
            try
            {
                var sendBytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(envelope));
                var properties = channel.CreateBasicProperties();

                properties.Persistent = true;
                foreach (var header in envelope.Headers)
                {
                    properties.Headers.Add(header.Key, header.Value);
                }

                foreach (var exchange in _rmqSettings.Exchanges)
                {
                    channel.BasicPublish(exchange.Name, string.Empty, properties, sendBytes);
                }
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}