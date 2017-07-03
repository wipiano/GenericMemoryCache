using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace GenericMemoryCache
{
    public static class CacheEntryExtensions
    {
        public static ICacheEntry<TKey, TValue> ToGeneric<TKey, TValue>(this ICacheEntry entry)
            => new CacheEntry<TKey, TValue>(entry);

        /// <summary>
        /// Sets the priority for keeping the cache entry in the cache during a memory pressure tokened cleanup.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="priority"></param>
        public static ICacheEntry<TKey, TValue> SetPriority<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            CacheItemPriority priority)
        {
            entry.Priority = priority;
            return entry;
        }

        /// <summary>
        /// Expire the cache entry if the given <see cref="IChangeToken"/> expires.
        /// </summary>
        /// <param name="entry">The <see cref="ICacheEntry<TKey, TValue>"/>.</param>
        /// <param name="expirationToken">The <see cref="IChangeToken"/> that causes the cache entry to expire.</param>
        public static ICacheEntry<TKey, TValue> AddExpirationToken<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            IChangeToken expirationToken)
        {
            if (expirationToken == null)
            {
                throw new ArgumentNullException(nameof(expirationToken));
            }

            entry.ExpirationTokens.Add(expirationToken);
            return entry;
        }

        /// <summary>
        /// Sets an absolute expiration time, relative to now.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="relative"></param>
        public static ICacheEntry<TKey, TValue> SetAbsoluteExpiration<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            TimeSpan relative)
        {
            entry.AbsoluteExpirationRelativeToNow = relative;
            return entry;
        }

        /// <summary>
        /// Sets an absolute expiration date for the cache entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="absolute"></param>
        public static ICacheEntry<TKey, TValue> SetAbsoluteExpiration<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            DateTimeOffset absolute)
        {
            entry.AbsoluteExpiration = absolute;
            return entry;
        }

        /// <summary>
        /// Sets how long the cache entry can be inactive (e.g. not accessed) before it will be removed.
        /// This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="offset"></param>
        public static ICacheEntry<TKey, TValue> SetSlidingExpiration<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            TimeSpan offset)
        {
            entry.SlidingExpiration = offset;
            return entry;
        }

        /// <summary>
        /// The given callback will be fired after the cache entry is evicted from the cache.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="callback"></param>
        public static ICacheEntry<TKey, TValue> RegisterPostEvictionCallback<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            PostEvictionDelegate callback)
        {
            entry.Entry.RegisterPostEvictionCallback(callback);
            return entry;
        }

        /// <summary>
        /// The given callback will be fired after the cache entry is evicted from the cache.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public static ICacheEntry<TKey, TValue> RegisterPostEvictionCallback<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            PostEvictionDelegate callback,
            object state)
        {
            entry.Entry.RegisterPostEvictionCallback(callback, state);
            return entry;
        }

        /// <summary>
        /// Sets the value of the cache entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="value"></param>
        public static ICacheEntry<TKey, TValue> SetValue<TKey, TValue>(
            this ICacheEntry<TKey, TValue> entry,
            TValue value)
        {
            entry.Entry.SetValue(value);
            return entry;
        }

        /// <summary>
        /// Applies the values of an existing <see cref="MemoryCacheEntryOptions"/> to the entry.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="options"></param>
        public static ICacheEntry<TKey, TValue> SetOptions<TKey, TValue>(this ICacheEntry<TKey, TValue> entry, MemoryCacheEntryOptions options)
        {
            entry.Entry.SetOptions(options);
            return entry;
        }
    }
}