﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lykke.Bil2.Contract.Common;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.BlocksReader.Events
{
    /// <summary>
    /// "Transfer coins" transactions model.
    /// Should be published for each executed transaction in the block being read if
    /// integration uses “transfer coins” transactions model. Integration should either
    /// support “transfer coins” or “transfer amount” transactions model.
    /// </summary>
    [PublicAPI]
    public class TransferCoinsTransactionExecutedEvent
    {
        /// <summary>
        /// ID of the block.
        /// </summary>
        [JsonProperty("blockId")]
        public string BlockId { get; }

        /// <summary>
        /// Number of the transaction in the block.
        /// </summary>
        [JsonProperty("transactionNumber")]
        public int TransactionNumber { get; }

        /// <summary>
        /// ID of the transaction.
        /// </summary>
        [JsonProperty("transactionId")]
        public string TransactionId { get; }

        /// <summary>
        /// Coins which were received within the transaction.
        /// </summary>
        [JsonProperty("receivedCoins")]
        public IReadOnlyCollection<ReceivedCoin> ReceivedCoins { get; }

        /// <summary>
        /// Coins which were spent within the transaction.
        /// </summary>
        [JsonProperty("spentCoins")]
        public IReadOnlyCollection<CoinReference> SpentCoins { get; }

        /// <summary>
        /// Optional.
        /// Fees in the particular asset, that was spent for the transaction.
        /// Can be omitted, if fee can be determined from the received and spent coins.
        /// </summary>
        [CanBeNull]
        [JsonProperty("fees")]
        public IReadOnlyCollection<Fee> Fees { get; }

        /// <summary>
        /// Optional.
        /// Flag which indicates, if transaction is irreversible.
        /// </summary>
        [CanBeNull]
        [JsonProperty("isIrreversible")]
        public bool? IsIrreversible { get; }

        /// <summary>
        /// "Transfer coins" transactions model.
        /// Should be published for each executed transaction in the block being read if
        /// integration uses “transfer coins” transactions model. Integration should either
        /// support “transfer coins” or “transfer amount” transactions model.
        /// </summary>
        /// <param name="blockId">ID of the block.</param>
        /// <param name="transactionNumber">Number of the transaction in the block.</param>
        /// <param name="transactionId">ID of the transaction.</param>
        /// <param name="receivedCoins">Coins which were received within the transaction.</param>
        /// <param name="spentCoins">Coins which were spent within the transaction.</param>
        /// <param name="fees">
        /// Optional.
        /// Fees in the particular asset, that was spent for the transaction.
        /// Can be omitted, if fee can be determined from the received and spent coins.
        /// </param>
        /// <param name="isIrreversible">
        /// Optional.
        /// Flag which indicates, if transaction is irreversible.
        /// </param>
        public TransferCoinsTransactionExecutedEvent(
            string blockId,
            int transactionNumber,
            string transactionId,
            IReadOnlyCollection<ReceivedCoin> receivedCoins,
            IReadOnlyCollection<CoinReference> spentCoins,
            IReadOnlyCollection<Fee> fees = null,
            bool? isIrreversible = null)
        {
            if (string.IsNullOrWhiteSpace(blockId))
                throw new ArgumentException("Should be not empty string", nameof(blockId));

            if (transactionNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(transactionNumber), transactionNumber, "Should be zero or positive number");

            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("Should be not empty string", nameof(transactionId));

            if (fees != null)
                FeesValidator.ValidateFees(fees, isRequest: false);

            BlockId = blockId;
            TransactionNumber = transactionNumber;
            TransactionId = transactionId;
            ReceivedCoins = receivedCoins ?? throw new ArgumentNullException(nameof(receivedCoins));
            SpentCoins = spentCoins ?? throw new ArgumentNullException(nameof(spentCoins));
            Fees = fees;
            IsIrreversible = isIrreversible;
        }
    }
}
