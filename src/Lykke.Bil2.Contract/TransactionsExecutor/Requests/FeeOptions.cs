﻿using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.Bil2.Contract.Common;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.TransactionsExecutor.Requests
{
    /// <summary>
    /// Transaction fee options
    /// </summary>
    [PublicAPI]
    public class FeeOptions
    {
        /// <summary>
        /// Type of the fee.
        /// </summary>
        [JsonProperty("type")]
        public FeeType Type { get; }

        /// <summary>
        /// Optional.
        /// Fee options for particular asset ID.
        /// </summary>
        [CanBeNull]
        [JsonProperty("assetOptions")]
        public IReadOnlyDictionary<AssetId, AssetFeeOptions> AssetOptions { get; }

        /// <summary>
        /// Transaction fee options
        /// </summary>
        public FeeOptions(FeeType type, IReadOnlyDictionary<AssetId, AssetFeeOptions> assetOptions = null)
        {
            Type = type;
            AssetOptions = assetOptions;
        }
    }
}