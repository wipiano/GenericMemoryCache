using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace GenericMemoryCache
{
    public static class MemoryCacheExtensions
    {
        public static MemoryCache<TKey, TValue> ToGeneric<TKey, TValue>(this IMemoryCache cache)
            => new MemoryCache<TKey, TValue>(cache);

        public static TValue Get<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key)
        {
            cache.TryGetValue(key, out TValue value);
            return value;
        }

        public static TValue Set<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key, TValue value)
        {
            cache.Cache.Set(key, value);
            return value;
        }

        public static TValue Set<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key, TValue value, DateTimeOffset absoluteExpiration)
        {
            cache.Cache.Set(key, value, absoluteExpiration);
            return value;
        }

        public static TValue Set<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key, TValue value, TimeSpan absoluteExpirationRelativeToNow)
        {
            cache.Cache.Set(key, value, absoluteExpirationRelativeToNow);
            return value;
        }

        public static TValue Set<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key, TValue value, IChangeToken expirationToken)
        {
            cache.Cache.Set(key, value, expirationToken);
            return value;
        }

        public static TValue Set<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key, TValue value, MemoryCacheEntryOptions options)
        {
            cache.Cache.Set(key, value, options);
            return value;
        }

        public static TValue GetOrCreate<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key,
            Func<ICacheEntry<TKey, TValue>, TValue> factory)
        {
            TValue ObjectFactory(ICacheEntry entry)
                => factory(entry.ToGeneric<TKey, TValue>());

            return cache.Cache.GetOrCreate<TValue>(key, ObjectFactory);
        }

        public static async Task<TValue> GetOrCreateAsync<TKey, TValue>(this IMemoryCache<TKey, TValue> cache, TKey key,
            Func<ICacheEntry<TKey, TValue>, Task<TValue>> factory)
        {
            Task<TValue> ObjectFactory(ICacheEntry entry)
                => factory(new CacheEntry<TKey, TValue>(entry));

            return await cache.Cache.GetOrCreateAsync(key, ObjectFactory);
        }
    }
}