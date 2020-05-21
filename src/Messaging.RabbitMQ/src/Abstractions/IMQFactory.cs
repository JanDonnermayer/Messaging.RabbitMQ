
namespace Messaging.RabbitMQ
{
    public interface IMQFactory
    {
        IMQConsumer<TMessage> GetConsumer<TMessage>(string queueName)
            where TMessage : class;

        IMQPublisher<TMessage> GetPublisher<TMessage>(string queueName)
            where TMessage : class;
    }
}

