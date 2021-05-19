using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Megarender.DataBus.Enums;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Megarender.DataBus
{
    public class RMQMessageConsumerService: IMessageConsumerService
    {
        private readonly DefaultObjectPool<IModel> _objectPool;
        private readonly RMQSettings _rmqSettings;

        public RMQMessageConsumerService(IPooledObjectPolicy<IModel> objectPolicy, RMQSettings rmqSettings)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);  ;
            _rmqSettings = rmqSettings;
        }

        public void Subscribe(Func<object,bool> handler)
        {
            var channel = _objectPool.Get();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {                
                var bytes = ea.Body.ToArray();
                var text = Encoding.UTF8.GetString(bytes);
                var eventType = Encoding.UTF8.GetString(ea.BasicProperties.Headers[DefaultHeaders.EventType.GetDescription()] as byte[]);
                var currentType = typeof(IEvent).Assembly.GetTypes().Single(t => t.Name.Equals(eventType));
                var currentEnvelope = typeof(Envelope<>).MakeGenericType(currentType);
                var envelope = System.Text.Json.JsonSerializer.Deserialize(text, currentEnvelope);
                var status = handler(envelope);
            };

            foreach (var queue in _rmqSettings.Queues)
            {
                channel.BasicConsume(queue.Name, false, consumer);
            }
        }
    }
}