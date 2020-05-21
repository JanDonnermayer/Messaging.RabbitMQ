namespace Genet.PDM.Workflows.RabbitMQ
{
    public interface IMQPublisher<TMessage> where TMessage : class
    {
        void Next(TMessage message);
    }
}

