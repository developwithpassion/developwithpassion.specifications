using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Machine.Specifications.Utility.Internal;

namespace developwithpassion.specifications.extensions
{
    public static class EnumerableAssertionExtensions
    {
        public static void ShouldContainOnlyInOrder<T>(this IEnumerable<T> items, params T[] ordered_items)
        {
            items.ShouldContainOnlyInOrder((IEnumerable<T>)ordered_items);
        }

        public static void ShouldContainOnlyInOrder<T>(this IEnumerable<T> items, IEnumerable<T> ordered_items)
        {
            var source = new List<T>(items);
            var it = ordered_items.GetEnumerator();
            var index = 0;

            while (it.MoveNext())
            {
                if (index >= source.Count)
                {
                    throw new SpecificationException(
                        "The set of items should only contain the items in the order {0}\r\nbut it is actually shorter and does not contain: {1}"
                            .format_using(ordered_items.EachToUsefulString(), ordered_items.Except(items).EachToUsefulString()));
                }

                if (!source[index].Equals(it.Current))
                {
                    throw new SpecificationException(
                        "The set of items should only contain the items in the order {0}\r\nbut it actually contains the items: {1}"
                            .format_using(ordered_items.EachToUsefulString(), items.EachToUsefulString()));
                }

                ++index;
            }

            if (index < source.Count)
            {
                throw new SpecificationException(
                    "The set of items should only contain the items in the order {0}\r\nbut it is actually longer and additionally contains: {1}"
                        .format_using(ordered_items.EachToUsefulString(), items.Except(ordered_items).EachToUsefulString()));

            }
        }
    }
}