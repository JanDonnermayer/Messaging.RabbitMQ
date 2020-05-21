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
    internal sealed class RabbitMQConsumer<TMessage> : IDisposable, IChannelReader<TMessage>
        where TMessage : class
    {
        private readonly DisposeRoutine disposeRoutine;

        private readonly Lazy<IObservable<TMessage>> channel;

        internal RabbitMQConsumer(IConnectionFactory connectionFactory, string queueName)
        {
            disposeRoutine = new DisposeRoutine();
            channel = new Lazy<IObservable<TMessage>>(GetMessages);

            IObservable<TMessage> GetMessages()
            {
                var channel = connectionFactory
                    .CreateConnection()
                    .CreateModel();

                disposeRoutine.Append(channel);

                channel.QueueDeclare(
                    queue: queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new EventingBasicConsumer(channel);
                var queue = new BlockingCollection<BasicDeliverEventArgs>(1);

                disposeRoutine.Append(queue);

                static string GetString(byte[] message) =>
                    Encoding.UTF8.GetString(message);

                static TMessage Deserialize(string message) =>
                    JsonConvert.DeserializeObject<TMessage>(message);

                void Handle(object model, BasicDeliverEventArgs ea) =>
                    queue.Add(ea);

                channel.BasicConsume(
                    queue: queueName,
                    autoAck: true,
                    consumer: consumer
                );

                consumer.Received += Handle;
                disposeRoutine.Append(
                    () => consumer.Received -= Handle
                );

                return queue
                    .GetConsumingEnumerable(disposeRoutine.CancellationToken)
                    .ToObservable()
                    .ObserveOn(TaskPoolScheduler.Default)
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .Select(ea => ea.Body)
                    .Select(GetString)
                    .Select(Deserialize);
            }
        }

        public IObservable<TMessage> Read() =>
            channel.Value;

        public void Dispose() =>
            disposeRoutine.Dispose();
    }
}

