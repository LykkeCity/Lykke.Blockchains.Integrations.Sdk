﻿using System;
using Lykke.Bil2.SharedDomain;
using Newtonsoft.Json;

namespace Lykke.Bil2.Contract.Common.Cryptography
{
    internal class AesKeysEnvelope
    {
        [JsonProperty("key")]
        public Base58String Key { get; }
        
        [JsonProperty("iv")]
        public Base58String Iv { get; }

        public AesKeysEnvelope(Base58String key, Base58String iv)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Iv = iv ?? throw new ArgumentNullException(nameof(iv));
        }
    }
}
