
namespace Messaging.RabbitMQ
{
    public interface IChannelFactory
    {
        IChannelReader<TMessage> CreateChannelReader<TMessage>(string channelName)
            where TMessage : class;

        IChannelWriter<TMessage> CreateChannelWriter<TMessage>(string channelName)
            where TMessage : class;
    }
}

