using System;
using System.Threading;

namespace Messaging.RabbitMQ
{
    public interface IMQConsumer<TMessage> where TMessage : class
    {
        IObservable<TMessage> GetMessages(CancellationToken ct);
    }
}

