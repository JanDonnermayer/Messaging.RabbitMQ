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
                .CreateChannelFactory();

            const string QUEUE = "queue1";

            // Act
            const string SENT = "Hello!";
            factory
                .CreateChannelWriter<string>(QUEUE)
                .Write(SENT);

            string mut_received = string.Empty;
            factory
                .CreateChannelReader<string>(QUEUE)
                .Read()
                .Do(msg => mut_received = msg)
                .Subscribe();

            await Task
                .Delay(1000)
                .ConfigureAwait(false);

            // Assert
            Assert.AreEqual(SENT, mut_received);
        }
    }
}