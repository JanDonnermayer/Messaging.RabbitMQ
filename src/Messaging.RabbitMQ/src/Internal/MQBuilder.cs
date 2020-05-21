
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.RabbitMQ
{
    internal sealed class MQBuilder : IChannelBuilder
    {
        private readonly IServiceCollection services;

        private readonly IChannelFactory factory;

        public MQBuilder(
            IServiceCollection services,
            IChannelFactory factory
        )
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IChannelBuilder AddWriter<TMessage>(string queueName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.CreateChannelWriter<TMessage>(queueName)
            );
            return this;
        }

        public IChannelBuilder AddReader<TMessage>(string queueName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.CreateChannelReader<TMessage>(queueName)
            );
            return this;
        }
    }
}

