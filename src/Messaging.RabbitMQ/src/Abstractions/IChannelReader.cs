using System;
using System.Threading;

namespace Messaging.RabbitMQ
{
    public interface IChannelReader<TMessage> where TMessage : class
    {
        IObservable<TMessage> Read();
    }
}

