﻿using System;
using JetBrains.Annotations;
using Lykke.Bil2.Sdk.SignService.Services;
using Lykke.Bil2.Sdk.SignService.Settings;
using Lykke.SettingsReader;

namespace Lykke.Bil2.Sdk.SignService
{
    /// <summary>
    /// Service provider options for a blockchain integration sign service.
    /// </summary>
    [PublicAPI]
    public class SignServiceOptions<TAppSettings>

        where TAppSettings : BaseSignServiceSettings
    {
        /// <summary>
        /// Required.
        /// Name of the blockchain integration in CamelCase
        /// </summary>
        public string IntegrationName { get; set; }

        /// <summary>
        /// Optional.
        /// Provides options to access application settings.
        /// </summary>
        [CanBeNull]
        public Action<IReloadingManager<TAppSettings>> UseSettings { get; set; }

        /// <summary>
        /// Required.
        /// <see cref="IAddressGenerator"/> implementation factory.
        /// </summary>
        public Func<ServiceFactoryContext<TAppSettings>, IAddressGenerator> AddressGeneratorFactory { get; set; }

        /// <summary>
        /// Required.
        /// <see cref="ITransactionSigner"/> implementation factory.
        /// </summary>
        public Func<ServiceFactoryContext<TAppSettings>, ITransactionSigner> TransactionSignerFactory { get;set; }

        
    }
}
