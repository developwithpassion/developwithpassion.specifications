using System;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.core
{
  [Subject(typeof(BlockThat))]
  public class BlockThatSpecs
  {
    public abstract class concern : use_engine<RhinoFakeEngine>.observe
    {
    }

    public class when_ignoring_the_exceptions_around_an_action_block : concern
    {
      Because b = () =>
      {
        BlockThat.ignores_exceptions(() =>
        {
          throw new NotImplementedException();
        });
      };

      It should_execute_the_original_action_without_throwing_an_exception = () =>
      {
        true.ShouldBeTrue();
      };
    }

    public class when_ignoring_exceptions_around_a_func_block : concern
    {
      public class and_the_original_block_does_not_throw_an_exception : when_ignoring_exceptions_around_a_func_block
      {
        Because b = () =>
          result = BlockThat.ignores_exceptions(() => 2);

        It should_return_the_return_value_of_the_original_block = () =>
          result.ShouldEqual(2);

        static int result;
      }

      public class and_the_original_block_throws_an_exception : when_ignoring_exceptions_around_a_func_block
      {
        public class and_no_default_value_was_specified : and_the_original_block_throws_an_exception
        {
          Because b = () =>
            result = BlockThat.ignores_exceptions(() =>
            {
              throw new NotImplementedException();
              return 2;
            });

          It should_return_the_default_value_for_the_return_type = () =>
            result.ShouldEqual(default(int));
        }

        public class and_a_default_value_was_provided : and_the_original_block_throws_an_exception
        {
          Because b = () =>
            result = BlockThat.ignores_exceptions(() =>
            {
              throw new NotImplementedException();
              return 2;
            }, 42);

          It should_return_the_default_value_for_the_return_type = () =>
            result.ShouldEqual(42);
        }

        static int result;
      }
    }
  }
}