
namespace Messaging.RabbitMQ
{
    public interface IChannel
    {
        IChannelReader<TMessage> CreateReader<TMessage>(string channelName)
            where TMessage : class;

        IChannelWriter<TMessage> CreateWriter<TMessage>(string channelName)
            where TMessage : class;
    }
}

