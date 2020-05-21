
namespace Messaging.RabbitMQ
{
    internal interface IMQBuilder
    {
        IMQBuilder AddConsumer<TMessage>(string queueName) where TMessage : class;
        IMQBuilder AddPublisher<TMessage>(string queueName) where TMessage : class;
    }
}

