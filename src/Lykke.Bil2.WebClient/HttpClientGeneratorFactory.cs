﻿using System;
using JetBrains.Annotations;

namespace Lykke.Bil2.WebClient
{
    /// <summary>
    /// Blockchain integration HTTP client generator factory
    /// </summary>
    [PublicAPI]
    public static class HttpClientGeneratorFactory
    {
        /// <summary>
        /// Creates blockchain integration HTTP client generator
        /// </summary>
        public static HttpClientGenerator.HttpClientGenerator Create(Action<HttpClientGeneratorOptions> optionConfiguration)
        {
            if (optionConfiguration == null)
                throw new ArgumentNullException(nameof(optionConfiguration));

            // TODO: Request timeout
            var options = new HttpClientGeneratorOptions();
            optionConfiguration(options);

            var clientBuilder = HttpClientGenerator.HttpClientGenerator.BuildForUrl(options.Url)
                .WithoutRetries()
                .WithoutCaching()
                .WithAdditionalCallsWrapper(new MapExceptionCallWrapper())
                .WithAdditionalDelegatingHandler(new LogHttpRequestErrorHandler(options.LogFactory));

            foreach (var handler in options?.Handlers)
            {
                clientBuilder.WithAdditionalDelegatingHandler(handler);
            }

            return clientBuilder.Create();
        }
    }
}