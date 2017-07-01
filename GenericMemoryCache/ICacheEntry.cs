using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace GenericMemoryCache
{
    /// <summary>
    /// Generic CacheEntry
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface ICacheEntry<out TKey, out TValue>
    {
        ICacheEntry Entry { get; }

        TKey Key { get; }
        TValue Value { get; }
        DateTimeOffset? AbsoluteExpiration { get; set; }
        TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        TimeSpan? SlidingExpiration { get; set; }
        IList<IChangeToken> ExpirationTokens { get; }
        IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }
        CacheItemPriority Priority { get; set; }
    }
}