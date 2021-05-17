using System;
using System.Text;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
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
                var dt = _rmqSettings.Exchange.Type.ToString().ToLowerInvariant();
                channel.ExchangeDeclare(_rmqSettings.Exchange.Name, _rmqSettings.Exchange.Type.ToString().ToLowerInvariant(), true, false, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(envelope.Message));
                var properties = channel.CreateBasicProperties();

                properties.Persistent = true;
                foreach (var header in envelope.Headers)
                {
                    properties.Headers.Add(header.Key, header.Value);
                }

                channel.BasicPublish(_rmqSettings.Exchange.Name, routingKey, properties, sendBytes);
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}