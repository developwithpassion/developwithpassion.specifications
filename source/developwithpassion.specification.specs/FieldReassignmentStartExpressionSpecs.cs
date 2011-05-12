using System.Reflection;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace developwithpassion.specification.specs
{
  public class FieldReassignmentStartExpressionSpecs
  {
    public class TypeWithAStaticField
    {
      public static string some_value = "blah";
      public static int some_value_that_will_be_boxed = 0x42;
    }

    public abstract class concern : Observes<FieldReassignmentStartExpression>
    {
      Establish c = delegate
      {
        member_info = typeof(TypeWithAStaticField).GetField("some_value");
        switcher_factory = depends.on<FieldSwitcherFactory>();
        switcher = fake.an<ISwapValues>();
        switcher_factory.setup(x => x.create_to_target(Arg<MemberInfo>.Is.NotNull)).Return(switcher);
      };

      protected static MemberInfo member_info;
      protected static ISwapValues switcher;
      protected static FieldSwitcherFactory switcher_factory;
    }

    [Subject(typeof(FieldReassignmentStartExpression))]
    public class when_changing_the_target_that_requires_a_boxing_operation_to_be_performed : concern
    {
      Establish c = () =>
      {
        var target = typeof(TypeWithAStaticField);
        boxed_member_info = typeof(TypeWithAStaticField).GetField("some_value_that_will_be_boxed");
      };

      Because b = () =>
        result = sut.change(() => TypeWithAStaticField.some_value_that_will_be_boxed);

      It should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
        result.ShouldEqual(switcher);

      static FieldInfo boxed_member_info;
      static ISwapValues result;
    }

    [Subject(typeof(FieldReassignmentStartExpression))]
    public class when_provided_the_target_which_it_is_going_to_be_changing : concern
    {
      Because b = () =>
        result = sut.change(() => TypeWithAStaticField.some_value);

      It should_return_a_field_changer_that_can_be_used_to_specify_the_value_for_during_testing = () =>
        result.ShouldEqual(switcher);

      static ISwapValues result;
    }
  }
}