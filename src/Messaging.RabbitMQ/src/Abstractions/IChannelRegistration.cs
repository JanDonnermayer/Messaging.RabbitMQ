
namespace Messaging.RabbitMQ
{
    internal interface IChannelRegistration
    {
        IChannelRegistration AddReader<TMessage>(string channelName)
            where TMessage : class;

        IChannelRegistration AddWriter<TMessage>(string channelName)
            where TMessage : class;
    }
}

