﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.Bil2.Contract.Common;
using Lykke.Bil2.SharedDomain;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.TransactionsExecutor.Responses
{
    /// <summary>
    /// Endpoints:
    /// - [POST] /api/transactions/estimated/transfers/amount
    /// - [POST] /api/transactions/estimated/transfers/coins
    /// </summary>
    public class EstimateTransactionResponse
    {
        /// <summary>
        /// Estimated transaction fees for the particular asset.
        /// </summary>
        [JsonProperty("estimatedFees")]
        public IReadOnlyCollection<Fee> EstimatedFees { get; }

        /// <summary>
        /// Optional.
        /// Object describing an failure if it can be mapped to the <see cref="TransactionEstimationError"/>.
        /// </summary>
        [CanBeNull]
        [JsonProperty("error")]
        public TransactionEstimationFailure Error { get; }

        /// <summary>
        /// Endpoints:
        /// - [POST] /api/transactions/estimated/transfers/amount
        /// - [POST] /api/transactions/estimated/transfers/coins
        /// </summary>
        /// <param name="estimatedFees">Estimated transaction fee for the particular asset.</param>
        /// <param name="error">
        /// Optional.
        /// Object describing an failure if it can be mapped to the <see cref="TransactionEstimationError"/>.
        /// </param>
        public EstimateTransactionResponse(IReadOnlyCollection<Fee> estimatedFees, TransactionEstimationFailure error = null)
        {
            if(estimatedFees == null)
                throw new ArgumentNullException(nameof(estimatedFees));

            FeesValidator.ValidateFeesInResponse(estimatedFees);

            EstimatedFees = estimatedFees;
            Error = error;
        }
    }
}
