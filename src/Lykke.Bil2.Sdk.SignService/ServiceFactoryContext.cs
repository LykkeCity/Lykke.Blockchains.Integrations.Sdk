﻿using System;
using JetBrains.Annotations;
using Lykke.Bil2.Sdk.SignService.Settings;
using Lykke.SettingsReader;

namespace Lykke.Bil2.Sdk.SignService
{
    [PublicAPI]
    public class ServiceFactoryContext<TAppSettings>

        where TAppSettings : BaseSignServiceSettings
    {
        public IServiceProvider Services { get; }

        public IReloadingManager<TAppSettings> Settings { get; }

        public ServiceFactoryContext(IServiceProvider services, IReloadingManager<TAppSettings> settings)
        {
            Services = services;
            Settings = settings;
        }
    }
}
