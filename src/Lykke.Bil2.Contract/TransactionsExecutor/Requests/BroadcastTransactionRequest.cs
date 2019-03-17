﻿using JetBrains.Annotations;
using Lykke.Bil2.Contract.Common;
using Lykke.Bil2.Contract.Common.Exceptions;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.TransactionsExecutor.Requests
{
    /// <summary>
    /// Endpoint: [POST] /api/transactions/broadcasted
    /// </summary>
    [PublicAPI]
    public class BroadcastTransactionRequest
    {
        /// <summary>
        /// The signed transaction.
        /// </summary>
        [JsonProperty("signedTransaction")]
        public Base58String SignedTransaction { get; }

        /// <summary>
        /// Endpoint: [POST] /api/transactions/broadcasted
        /// </summary>
        /// <param name="signedTransaction">The signed transaction.</param> 
        public BroadcastTransactionRequest(Base58String signedTransaction)
        {
            if (string.IsNullOrWhiteSpace(signedTransaction?.ToString()))
                throw RequestValidationException.ShouldBeNotEmptyString(nameof(signedTransaction));

            SignedTransaction = signedTransaction;
        }
    }
}
