﻿using System;
using JetBrains.Annotations;
using Lykke.Bil2.RabbitMq.Publication;
using Lykke.Bil2.RabbitMq.Subscription;
using MessagePack;

namespace Lykke.Bil2.RabbitMq
{
    /// <summary>
    /// RabbitMq endpoint - represents RabbitMq connection and provides entry points
    /// to start listening for the messages and publish messages.
    /// </summary>
    [PublicAPI]
    public interface IRabbitMqEndpoint : IDisposable
    {
        /// <summary>
        /// Initializes endpoint. Should be called before any other methods
        /// </summary>
        void Initialize();

        /// <summary>
        /// Creates exchange if it does not exist or just ignore it if it already exists.
        /// </summary>
        void DeclareExchange(string exchangeName);

        /// <summary>
        /// Creates message publisher. Should be called after <see cref="Initialize"/>.
        /// </summary>
        IMessagePublisher CreatePublisher(string exchangeName, string correlationId = null);

        /// <summary>
        /// Registers custom formatter resolvers for this endpoint.
        /// </summary>
        void RegisterMessagePackFormatterResolvers(params IFormatterResolver[] resolvers);
        
        /// <summary>
        /// Subscribing for the messages. Should be called after <see cref="Initialize"/>.
        /// </summary>
        void Subscribe(IMessageSubscriptionsRegistry subscriptionsRegistry,
            string listeningExchangeName,
            string listeningRoute,
            TimeSpan? defaultFirstLevelRetryTimeout = null,
            TimeSpan? maxFirstLevelRetryMessageAge = null,
            int maxFirstLevelRetryCount = 5,
            int firstLevelRetryQueueCapacity = 10000,
            int processingQueueCapacity = 1000,
            int messageConsumersCount = 1,
            int messageProcessorsCount = 1,
            string replyExchangeName = null);

        /// <summary>
        /// Start listening for the subscribed message. Should be called after <see cref="Subscribe"/>.
        /// </summary>
        void StartListening();
    }
}
