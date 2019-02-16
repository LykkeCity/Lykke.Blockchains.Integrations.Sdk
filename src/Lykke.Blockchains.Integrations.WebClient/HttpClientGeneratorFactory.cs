﻿using System;
using JetBrains.Annotations;
using Lykke.Common.Log;
using Refit;

namespace Lykke.Blockchains.Integrations.WebClient
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
        public static HttpClientGenerator.HttpClientGenerator Create(string url, ILogFactory logFactory)
        {
            // TODO: Request timeout

            return HttpClientGenerator.HttpClientGenerator.BuildForUrl(url)
                .WithoutRetries()
                .WithoutCaching()
                .WithAdditionalCallsWrapper(new MapExceptionCallWrapper())
                .WithAdditionalDelegatingHandler(new LogHttpRequestErrorHandler(logFactory))
                .Create();
        }
    }
}
