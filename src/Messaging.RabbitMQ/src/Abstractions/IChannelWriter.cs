namespace Messaging.RabbitMQ
{
    public interface IChannelWriter<TMessage> where TMessage : class
    {
        void Write(TMessage message);
    }
}

