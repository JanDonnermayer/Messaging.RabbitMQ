using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Messaging.RabbitMQ
{
    internal sealed class RabbitMQPublisher<TMessage> : IDisposable, IMQPublisher<TMessage>
        where TMessage : class
    {
        private readonly Lazy<IModel> channel;

        private readonly string queueName;

        internal RabbitMQPublisher(IConnectionFactory connectionFactory, string queueName)
        {
            this.channel = new Lazy<IModel>(() =>
            {
                var channel = connectionFactory
                    .CreateConnection()
                    .CreateModel();

                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                return channel;
            });
            this.queueName = queueName;
        }

        public void Next(TMessage message)
        {
            static byte[] GetBytes(string message) =>
                Encoding.UTF8.GetBytes(message);

            static string Serialize(TMessage message) =>
                JsonConvert.SerializeObject(message);

            channel.Value.BasicPublish(
                exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: GetBytes(Serialize(message))
            );
        }

        public void Dispose()
        {
            if (!channel.IsValueCreated) return;
            channel.Value.Dispose();
        }
    }
}

