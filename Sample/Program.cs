using System;
using System.Collections.Generic;
using System.Threading;
using GenericMemoryCache;
using Microsoft.Extensions.Caching.Memory;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // non-generic MemoryCache instance
            IMemoryCache nonGeneric = new MemoryCache(new MemoryCacheOptions());

            // GenericMemoryCache
            IMemoryCache<int, Book> booksCache = nonGeneric.ToGeneric<int, Book>();

            Book CreateValue(ICacheEntry<int, Book> entry)
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(2));
                Console.WriteLine($"id = {entry.Key} created");
                return entry.Value;
            }

            // GetOrCreate
            Book asp1 = booksCache.GetOrCreate(1, CreateValue);
            // id = 1 created

            Book scala1 = booksCache.GetOrCreate(2, CreateValue);
            // id = 2 created

            // cached
            Book asp2 = booksCache.GetOrCreate(1, CreateValue);
            Console.WriteLine($"asp1 == asp2(cached): {asp1 == asp2}"); // true

            // Remove
            booksCache.Remove(2);

            // Create
            Book scala2 = booksCache.GetOrCreate(2, CreateValue);
            Console.WriteLine($"scala1 == scala2(created): {scala1 == scala2}"); // false

            // Expire
            Thread.Sleep(TimeSpan.FromSeconds(5));
            
            // Create
            Book scala3 = booksCache.GetOrCreate(2, CreateValue);
            // id = 2 created

            Console.WriteLine("end.");
            Console.ReadLine();
        }
    }

    // てきとうなデータベースのかわり
    public static class Database
    {
        private static Dictionary<int, Book> BooksDic = new Dictionary<int, Book>()
        {
            {
                1, new Book()
                {
                    Id = 1,
                    Title = "Programming ASP.NET",
                    Stock = 5
                }
            },
            {
                2, new Book()
                {
                    Id = 2,
                    Title = "Programming in Scala",
                    Stock = 10
                }
            }
        };

        public static Book SelectBook(int id) => BooksDic.TryGetValue(id, out Book book) ? book.DeepCopy() : null;
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Stock { get; set; }

        public Book DeepCopy()
            => new Book()
            {
                Id = this.Id,
                Title = this.Title,
                Stock = this.Stock
            };
    }
}