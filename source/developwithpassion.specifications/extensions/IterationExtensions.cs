using System;
using System.Collections.Generic;
using System.Linq;

namespace developwithpassion.specifications.extensions
{
    public static class IterationExtensions
    {
        public static void each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> one_at_a_time<T>(this IEnumerable<T> items)
        {
            return items.Select(x => x);
        }

        public static IEnumerable<int> to(this int start, int end)
        {
            return Enumerable.Range(start, end);
        }
    }
}