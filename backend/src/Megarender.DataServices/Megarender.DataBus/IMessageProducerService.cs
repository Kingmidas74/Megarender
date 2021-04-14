namespace Megarender.DataBus
{
    public interface IMessageProducerService
    {
        void Enqueue(string messageString, string routingKey);
    }
}