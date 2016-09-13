using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.extensions;
using Machine.Specifications;
using Machine.Specifications.Utility.Internal;

namespace developwithpassion.specifications.assertions.enumerables
{
  public static class AssertionExtensions
  {
    public static void contain_only_in_order<T>(this IProvideAccessToAssertions<IEnumerable<T>> extension_point,
      params T[] ordered_items)
    {
      extension_point.contain_only_in_order((IEnumerable<T>) ordered_items);
    }

    public static void contain_only_in_order<T>(this IProvideAccessToAssertions<IEnumerable<T>> items,
      IEnumerable<T> ordered_items)
    {
      var source = new List<T>(items.value);
      var it = ordered_items.GetEnumerator();
      var index = 0;

      while (it.MoveNext())
      {
        if (index >= source.Count)
        {
          throw new SpecificationException(
            "The set of items should only contain the items in the order {0}\r\nbut it is actually shorter and does not contain: {1}"
              .format_using(ordered_items.EachToUsefulString(), ordered_items.Except(items.value).EachToUsefulString()));
        }

        if (!source[index].Equals(it.Current))
        {
          throw new SpecificationException(
            "The set of items should only contain the items in the order {0}\r\nbut it actually contains the items: {1}"
              .format_using(ordered_items.EachToUsefulString(), items.value.EachToUsefulString()));
        }

        ++index;
      }

      if (index < source.Count)
      {
        throw new SpecificationException(
          "The set of items should only contain the items in the order {0}\r\nbut it is actually longer and additionally contains: {1}"
            .format_using(ordered_items.EachToUsefulString(), items.value.Except(ordered_items).EachToUsefulString()));
      }
    }
  }
}