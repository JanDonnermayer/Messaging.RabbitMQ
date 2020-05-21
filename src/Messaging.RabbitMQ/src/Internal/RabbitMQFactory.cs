
using System;
using RabbitMQ.Client;

namespace Genet.PDM.Workflows.RabbitMQ
{

    public class RabbitMQFactory : IMQFactory
    {
        private readonly ConnectionFactory connectionFactory;

        public RabbitMQFactory(ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null)
                throw new ArgumentNullException(nameof(connectionInfo));

            this.connectionFactory = new ConnectionFactory()
            {
                HostName = connectionInfo.HostName,
                UserName = connectionInfo.UserName,
                Password = connectionInfo.Password,
                Port = connectionInfo.Port
            };
        }

        internal RabbitMQConsumer<TMessage> GetConsumer<TMessage>(string queueName)
            where TMessage : class
        {
            if (queueName is null)
                throw new ArgumentNullException(nameof(queueName));

            return new RabbitMQConsumer<TMessage>(
                connectionFactory: connectionFactory,
                queueName: queueName
            );
        }

        internal RabbitMQPublisher<TMessage> GetPublisher<TMessage>(string queueName)
            where TMessage : class
        {
            if (queueName is null)
                throw new ArgumentNullException(nameof(queueName));

            return new RabbitMQPublisher<TMessage>(
                connectionFactory: connectionFactory,
                queueName: queueName
            );
        }

        IMQConsumer<TMessage> IMQFactory.GetConsumer<TMessage>(string queueName) =>
            GetConsumer<TMessage>(queueName);

        IMQPublisher<TMessage> IMQFactory.GetPublisher<TMessage>(string queueName) =>
            GetPublisher<TMessage>(queueName);
    }
}

