using System;
using System.Text;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reactive.Linq;
using System.Reactive.Concurrency;

namespace Messaging.RabbitMQ
{
    internal sealed class RabbitMQConsumer<TMessage> : IDisposable, IMQConsumer<TMessage>
        where TMessage : class
    {
        private readonly Lazy<IModel> channel;

        private readonly string queueName;

        internal RabbitMQConsumer(IConnectionFactory connectionFactory, string queueName)
        {
            this.queueName = queueName;
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
        }

        public IObservable<TMessage> GetMessages(CancellationToken ct)
        {
            var consumer = new EventingBasicConsumer(channel.Value);
            var queue = new BlockingCollection<byte[]>(1);

            static string GetString(byte[] message) =>
                Encoding.UTF8.GetString(message);

            static TMessage Deserialize(string message) =>
                JsonConvert.DeserializeObject<TMessage>(message);

            void Handle(object model, BasicDeliverEventArgs ea) =>
                queue.Add(ea.Body);

            channel.Value.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            consumer.Received += Handle;
            ct.Register(() => consumer.Received -= Handle);

            return queue
               .GetConsumingEnumerable(ct)
               .ToObservable()
               .ObserveOn(TaskPoolScheduler.Default)
               .Select(GetString)
               .Select(Deserialize);
        }

        public void Dispose()
        {
            if (!channel.IsValueCreated) return;
            channel.Value.Dispose();
        }
    }
}

