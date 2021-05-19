using System;
using System.Text;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
using Microsoft.Extensions.Logging;
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

        public void Enqueue<T>(T message) where T : IEvent
        {
            if (message == null)
                return;
            var channel = _objectPool.Get();
            try
            {                
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                var envelope = new Envelope<T> {
                    Headers = new System.Collections.Generic.Dictionary<string, string> {
                        {Megarender.DataBus.Enums.DefaultHeaders.EventType.GetDescription(), typeof(T).Name}
                    },
                    Message = message
                };
                var sendBytes = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(envelope));
                properties.Headers = new System.Collections.Generic.Dictionary<string, object>();
                foreach (var header in envelope.Headers)
                {
                    properties.Headers.Add(header.Key, header.Value);
                }

                foreach (var exchange in _rmqSettings.Exchanges)
                {
                    channel.BasicPublish(exchange.Name, string.Empty, properties, sendBytes);
                }
            }
            catch
            {
                
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}