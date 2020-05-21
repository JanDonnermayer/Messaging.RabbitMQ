
namespace Messaging.RabbitMQ
{
    public interface IChannelFactory
    {
        IChannelReader<TMessage> GetReader<TMessage>(string channelName)
            where TMessage : class;

        IChannelWriter<TMessage> GetWriter<TMessage>(string channelName)
            where TMessage : class;
    }
}

