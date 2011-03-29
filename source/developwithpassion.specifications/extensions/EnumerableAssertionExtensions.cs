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
            if (ordered_items.Where((item, index) => ! source[index].Equals(item)).Any())
            {
                throw new SpecificationException(
                    "The set of items should only contain the items in the order {0}\r\nbut it actually contains the items:{1}"
                        .format_using(new object[] {ordered_items.EachToUsefulString(), items.EachToUsefulString()}));
            }
        }
    }
}