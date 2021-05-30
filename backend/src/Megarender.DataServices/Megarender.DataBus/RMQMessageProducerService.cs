using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Megarender.DataBus.Enums;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
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
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
            _rmqSettings = rmqSettings;
        }

        public void Enqueue<T>(T message, Dictionary<string,string> headers) where T : IEvent
        {
            if (message == null)
                return;
            var channel = _objectPool.Get();
            try
            {                
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                headers.Add(DefaultHeaders.EventType.GetDescription(), typeof(T).Name);
                
                var envelope = new Envelope<T> {
                    Headers = headers,
                    Message = message
                };
                var sendBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(envelope));
                properties.Headers = new Dictionary<string, object>();
                foreach (var (key, value) in envelope.Headers)
                {
                    properties.Headers.Add(key, value);
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