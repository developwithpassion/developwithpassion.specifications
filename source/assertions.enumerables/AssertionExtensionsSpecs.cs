using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.assertions.enumerables
{
  public class AssertionExtensionsSpecs
  {
    public class concern : use_engine<MoqFakeEngine>.observe
    {
    }

    public class when_comparing_two_sets_of_items : concern
    {
      Establish c = () =>
        items = Enumerable.Range(1, 3).ToList();

      public class and_the_sets_have_different_items : when_comparing_two_sets_of_items
      {
        Because b = () =>
          spec.catch_exception(() => items.should().contain_only_in_order(3, 2, 1));

        It should_get_an_exception_when_trying_to_make_an_assertion = () =>
          spec.exception_thrown.should().be_an<SpecificationException>();
      }

      public class and_the_expected_set_is_longer_than_actual : when_comparing_two_sets_of_items
      {
        Because b = () =>
          spec.catch_exception(() => items.should().contain_only_in_order(1, 2, 3, 4));

        It should_get_an_exception_when_trying_to_make_an_assertion = () =>
          spec.exception_thrown.should().be_an<SpecificationException>();
      }

      public class and_the_expected_set_is_shorter_than_actual : when_comparing_two_sets_of_items
      {
        Because b = () =>
          spec.catch_exception(() => items.should().contain_only_in_order(1, 2));

        It should_get_an_exception_when_trying_to_make_an_assertion = () =>
          spec.exception_thrown.should().be_an<SpecificationException>();
      }

      public class and_the_sets_are_the_same : when_comparing_two_sets_of_items
      {
        Because b = () =>
          items.should().contain_only_in_order(1, 2, 3);
      };

      It should_not_get_an_exception_when_trying_to_make_the_assertion = () =>
      {
      };
    }

    static IList<int> items;
  }
}
