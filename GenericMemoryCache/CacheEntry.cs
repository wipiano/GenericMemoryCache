using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace GenericMemoryCache
{
    public class CacheEntry<TKey, TValue> : ICacheEntry<TKey, TValue>
    {
        public ICacheEntry Entry { get; }
        internal CacheEntry(ICacheEntry entry)
        {
            this.Entry = entry;
        }

        public TKey Key => (TKey)this.Entry.Key;
        public TValue Value => (TValue)this.Entry.Value;

        public DateTimeOffset? AbsoluteExpiration
        {
            get => this.Entry.AbsoluteExpiration;
            set => this.Entry.AbsoluteExpiration = value;
        }

        public TimeSpan? AbsoluteExpirationRelativeToNow
        {
            get => this.Entry.AbsoluteExpirationRelativeToNow;
            set => this.Entry.AbsoluteExpirationRelativeToNow = value;
        }

        public TimeSpan? SlidingExpiration
        {
            get => this.Entry.SlidingExpiration;
            set => this.Entry.SlidingExpiration = value;
        }

        public IList<IChangeToken> ExpirationTokens => this.Entry.ExpirationTokens;

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks
            => this.Entry.PostEvictionCallbacks;

        public CacheItemPriority Priority
        {
            get => this.Entry.Priority;
            set => this.Entry.Priority = value;
        }
    }
}