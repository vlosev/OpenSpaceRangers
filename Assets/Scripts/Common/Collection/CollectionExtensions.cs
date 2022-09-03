using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Collection
{
    public static class CollectionExtensions
    {
        //static random for all
        private static readonly Random Random = new();
        
        public static IEnumerable<T> ShuffleCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(i => Random.Next()).ToList();
        }
    }
}