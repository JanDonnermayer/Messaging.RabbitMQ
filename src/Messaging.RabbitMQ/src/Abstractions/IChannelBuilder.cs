
namespace Messaging.RabbitMQ
{
    internal interface IChannelBuilder
    {
        IChannelBuilder AddReader<TMessage>(string channelName) where TMessage : class;

        IChannelBuilder AddWriter<TMessage>(string channelName) where TMessage : class;
    }
}

