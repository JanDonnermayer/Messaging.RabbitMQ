using System;
using System.Threading;

namespace Genet.PDM.Workflows.RabbitMQ
{
    public interface IMQConsumer<TMessage> where TMessage : class
    {
        IObservable<TMessage> GetMessages(CancellationToken ct);
    }
}

