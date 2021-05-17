using System;
using System.Text;
using System.Threading.Tasks;
using Megarender.DataBus.Models;
using Megarender.Domain.Extensions;
using Microsoft.Extensions.ObjectPool;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
                foreach (var exchange in _rmqSettings.Exchanges)
                {
                    channel.ExchangeDeclare(exchange.Name, exchange.Type.ToString().ToLowerInvariant(), true, false, null);
                }

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(envelope.Message));
                var properties = channel.CreateBasicProperties();

                properties.Persistent = true;
                foreach (var header in envelope.Headers)
                {
                    properties.Headers.Add(header.Key, header.Value);
                }

                foreach (var exchange in _rmqSettings.Exchanges)
                {
                    channel.BasicPublish(exchange.Name, routingKey, properties, sendBytes);
                }
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }

        public void Subscribe<T>(Action<T> handler) where T:IMessage
        {
            var channel = _objectPool.Get();
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var envelope = JsonConvert.DeserializeObject<Envelope<T>>(Encoding.UTF8.GetString(body));
                handler(envelope.Message);
                await Task.Yield();

            };

            foreach (var queue in _rmqSettings.Queues)
            {
                channel.QueueDeclare(queue.Name);
                channel.BasicConsume(queue.Name, false, consumer);
            }
        }
    }
}