using System;
using System.Text;
using System.Threading.Tasks;
using Megarender.DataBus.Models;
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

        public void Subscribe<T>(Func<Envelope<T>,bool> handler) where T:IMessage
        {
            var channel = _objectPool.Get();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {                
                var bytes = ea.Body.ToArray();
                var text = Encoding.UTF8.GetString(bytes);
                var envelope = System.Text.Json.JsonSerializer.Deserialize<Envelope<T>>(text);
                var status = handler(envelope);
                await Task.Yield();
            };

            foreach (var queue in _rmqSettings.Queues)
            {
                channel.BasicConsume(queue.Name, false, consumer);
            }
        }
    }
}