
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.RabbitMQ
{
    internal sealed class ChannelRegistration : IChannelRegistration
    {
        private readonly IServiceCollection services;

        private readonly IChannel factory;

        public ChannelRegistration(
            IServiceCollection services,
            IChannel factory
        )
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public IChannelRegistration AddWriter<TMessage>(string channelName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.CreateWriter<TMessage>(channelName)
            );
            return this;
        }

        public IChannelRegistration AddReader<TMessage>(string channelName)
            where TMessage : class
        {
            services.AddSingleton(
                factory.CreateReader<TMessage>(channelName)
            );
            return this;
        }
    }
}

