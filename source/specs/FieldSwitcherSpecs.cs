using System;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.interactions;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.specs
{
  public class FieldSwitcherSpecs
  {
    public class concern : use_engine<MoqFakeEngine>.observe<ISwapValues, MemberTargetValueSwapper>
    {
      Establish c = () =>
      {
        original_value = "sdfsdfs";
        accessor = depends.on<MemberAccessor>();
        the_target_type = typeof(int);
        accessor.setup(x => x.declaring_type).Return(the_target_type);
      };

      protected static MemberAccessor accessor;
      protected static string original_value;
      protected static Type the_target_type;
    }

    [Subject(typeof(ISwapValues))]
    public class when_constructed : concern
    {
      It should_use_the_target_to_get_the_original_value = () =>
        accessor.should().received(x => x.get_value(the_target_type));
    }

    [Subject(typeof(ISwapValues))]
    public class when_provided_the_value_to_change_to : concern
    {
      Establish c = () =>
      {
        value_to_change_to = "sdfsdf";
        accessor.setup(x => x.get_value(the_target_type)).Return(original_value);
      };

      Because b = () =>
        result = sut.to(value_to_change_to);

      It should_provide_the_pipeline_pair_that_can_do_the_switching = () =>
      {
        result.setup();
        accessor.should().received(x => x.change_value_to(the_target_type, value_to_change_to));
        result.teardown();
        accessor.should().received(x => x.change_value_to(the_target_type, original_value));
      };

      protected static ObservationPair result;
      protected static string value_to_change_to;
    }
  }
}
