
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.RabbitMQ
{
    internal sealed class MQBuilder : IMQBuilder
    {
        private readonly IServiceCollection services;

        private readonly IMQFactory factory;

        public MQBuilder(
            IServiceCollection services,
            IMQFactory factory
        )
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IMQBuilder AddPublisher<TMessage>(string queueName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.GetPublisher<TMessage>(queueName)
            );
            return this;
        }

        public IMQBuilder AddConsumer<TMessage>(string queueName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.GetConsumer<TMessage>(queueName)
            );
            return this;
        }
    }
}

