using NUnit.Framework;

namespace Messaging.RabbitMQ.Test
{
    public class ConnectionInfoTests
    {
        [Test]
        [Category("Unit")]
        public void Test_CreateFactory()
        {
            Assert.DoesNotThrow(() =>
                ConnectionInfo.Default
                    .WithHostName("localhost")
                    .WithUserName("rabbitmq")
                    .WithPassword("rabbitmq")
                    .WithPort(5672)
                    .CreateMQFactory()
            );
        }
    }
}