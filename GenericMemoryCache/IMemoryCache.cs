using Microsoft.Extensions.Caching.Memory;

namespace GenericMemoryCache
{
    /// <summary>
    /// Generic な MemoryCache です
    /// </summary>
    /// <typeparam name="TKey">Type of cache key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface IMemoryCache<TKey, TValue>
    {
        IMemoryCache Cache { get; }

        CacheEntry<TKey, TValue> CreateEntry(TKey key);
        void Remove(TKey key);
        bool TryGetValue(TKey key, out TValue value);
    }
}
