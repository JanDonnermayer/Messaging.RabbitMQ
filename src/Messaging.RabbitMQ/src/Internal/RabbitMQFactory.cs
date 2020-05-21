
using System;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ
{
    internal sealed class RabbitMQFactory : IChannel
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

        internal RabbitMQReader<TMessage> CreateReader<TMessage>(string queueName)
            where TMessage : class
        {
            if (queueName is null)
                throw new ArgumentNullException(nameof(queueName));

            return new RabbitMQReader<TMessage>(
                connectionFactory: connectionFactory,
                queueName: queueName
            );
        }

        internal RabbitMQWriter<TMessage> CreateWriter<TMessage>(string queueName)
            where TMessage : class
        {
            if (queueName is null)
                throw new ArgumentNullException(nameof(queueName));

            return new RabbitMQWriter<TMessage>(
                connectionFactory: connectionFactory,
                queueName: queueName
            );
        }

        IChannelReader<TMessage> IChannel.CreateReader<TMessage>(string queueName) =>
            CreateReader<TMessage>(queueName);

        IChannelWriter<TMessage> IChannel.CreateWriter<TMessage>(string queueName) =>
            CreateWriter<TMessage>(queueName);
    }
}

