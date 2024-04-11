using System.Collections.Generic;
using System.Linq;
using System;
using Random = UnityEngine.Random;

namespace Game.Scripts.Extensions
{
    public static class EnumerableExtension
    {
        public static T PickRandom<T>(this IEnumerable<T> source) 
            => source.PickRandoms(1).First();
        
        public static IEnumerable<T> PickRandoms<T>(this IEnumerable<T> source, int count) 
            => source.ShuffleIE().Take(count);

        public static IEnumerable<T> ShuffleIE<T>(this IEnumerable<T> source) 
            => source.OrderBy(x => Guid.NewGuid());
        
        public static T PickRandomExcluding<T>(this IEnumerable<T> collection, T excludedItem)
        {
            if (collection == null)
                return default;

            List<T> actual = collection.Where(item => !item.Equals(excludedItem)).ToList();

            return actual.Count > 0 ? actual[Random.Range(0, actual.Count)] : default;
        }
    }
}