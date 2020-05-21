using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Messaging.RabbitMQ.Test
{
    public class MessagingIntegrationTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        [Category("Integration")]
        public async Task Test_Publish_Subscribe()
        {
            // Arrange
            var factory = ConnectionInfo.Default
                .WithHostName("localhost")
                .WithUserName("rabbitmq")
                .WithPassword("rabbitmq")
                .WithPort(5672)
                .CreateMQFactory();

            const string QUEUE = "queue1";

            var consumer = factory.GetConsumer<string>(QUEUE);
            var publisher = factory.GetPublisher<string>(QUEUE);

            // Act
            const string SENT = "Hello!";
            publisher.Next(SENT);

            string received = string.Empty;
            consumer
                .GetMessages(new CancellationTokenSource().Token)
                .Do(msg => received = msg)
                .Subscribe();

            await Task.Delay(1000);

            // Assert
            Assert.AreEqual(SENT, received);
        }
    }
}