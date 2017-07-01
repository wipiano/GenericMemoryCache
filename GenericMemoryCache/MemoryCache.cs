using System;
using Microsoft.Extensions.Caching.Memory;

namespace GenericMemoryCache
{
    public class MemoryCache<TKey, TValue> : IMemoryCache<TKey, TValue>, IDisposable
    {
        public IMemoryCache Cache { get; }

        internal MemoryCache(IMemoryCache cache)
        {
            this.Cache = cache;
        }

        //public int Count => Cache.Count;

        //public void Compact(double percentage) => Cache.Compact(percentage);

        public CacheEntry<TKey, TValue> CreateEntry(TKey key)
            => new CacheEntry<TKey,TValue>(this.Cache.CreateEntry(key));

        public void Dispose() => this.Cache.Dispose();

        public void Remove(TKey key) => this.Cache.Remove(key);

        public bool TryGetValue(TKey key, out TValue result)
            => this.Cache.TryGetValue<TValue>(key, out result);
    }
}