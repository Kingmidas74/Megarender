using System.Text;
using Megarender.DataBus.Enums;
using RabbitMQ.Client;
using Megarender.Domain.Extensions;
using System;

namespace Megarender.DataBus {
    public class MessageProducerService {
        IModel Channel { get; }
        public MessageProducerService (IModel rabbitMQChannel) {
            this.Channel = rabbitMQChannel;
        }
        public void Enqueue (string messageString, string routingKey) {
            var body = Encoding.UTF8.GetBytes ("message from webapi " + messageString);
            Channel.BasicPublish (AMQPExchanges.DIRECT.GetDescription(), routingKey, null, body);
        }

        public void Enqueue(string v, object p)
        {
            throw new NotImplementedException();
        }
    }
}