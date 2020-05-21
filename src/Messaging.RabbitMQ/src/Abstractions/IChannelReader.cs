using System;

namespace Messaging.RabbitMQ
{
    public interface IChannelReader<TMessage> where TMessage : class
    {
        IObservable<TMessage> Read();
    }
}

