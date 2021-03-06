﻿using JetBrains.Annotations;
using Lykke.Bil2.SharedDomain;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.TransactionsExecutor.Responses
{
    /// <summary>
    /// Endpoint: [GET] /api/addresses/{address}/validity?[tag=string]
    /// </summary>
    [PublicAPI]
    public class AddressValidityResponse
    {
        /// <summary>
        /// Result of the address validation.
        /// </summary>
        [JsonProperty("result")]
        public AddressValidationResult Result { get; }

        /// <summary>
        /// Endpoint: [GET] /api/addresses/{address}/validity?[tag=string]
        /// </summary>
        /// <param name="result">Result of the address validation.</param>
        public AddressValidityResponse(AddressValidationResult result)
        {
            Result = result;
        }
    }
}
