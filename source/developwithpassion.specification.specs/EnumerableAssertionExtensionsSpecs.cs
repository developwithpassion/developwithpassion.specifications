using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
  public class EnumerableAssertionExtensionsSpecs
  {
    public class concern : Observes
    {
    }

    public class when_comparing_two_sets_of_items : concern
    {
      Establish c = () =>
        items = Enumerable.Range(1, 3).ToList();

      public class and_the_sets_are_different : when_comparing_two_sets_of_items
      {
        Because b = () =>
          spec.catch_exception(() => items.ShouldContainOnlyInOrder(3, 2, 1));

        It should_get_an_exception_when_trying_to_make_an_assertion = () =>
          spec.exception_thrown.ShouldBeAn<SpecificationException>();
      }

      public class and_the_sets_are_the_same : when_comparing_two_sets_of_items
      {
        Because b = () =>
          items.ShouldContainOnlyInOrder(1, 2, 3);

        It should_not_get_an_exception_when_trying_to_make_the_assertion = () => { };
      }
    }

    static IList<int> items;
  }
}