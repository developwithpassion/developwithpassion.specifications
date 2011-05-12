using System.Reflection;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
  public class FieldMemberTargetSpecs
  {
    public class TheItem
    {
      public static string static_value = "lah";
    }

    public abstract class concern : Observes<MemberTarget, FieldMemberTarget>
    {
      Establish c = delegate
      {
        member = typeof(TheItem).GetMember("static_value")[0];
        depends.on(member);
      };

      protected static MemberInfo member;
    }

    [Subject(typeof(FieldMemberTarget))]
    public class when_getting_Its_value : concern
    {
      Because b = () =>
        result = sut.get_value();

      It should_get_the_value_of_the_field = () =>
        result.ShouldEqual(TheItem.static_value);

      protected static object result;
    }

    [Subject(typeof(FieldMemberTarget))]
    public class when_setting_Its_value : concern
    {
      Establish c = () =>
      {
        original_value = TheItem.static_value;
        value_to_change_to = "testing";
      };

      Because b = () =>
        sut.change_value_to(value_to_change_to);

      It should_change_the_value_of_the_field = () =>
        TheItem.static_value.ShouldEqual(value_to_change_to);

      Cleanup cleanup = () =>
        TheItem.static_value = original_value;

      protected static string original_value;
      static string value_to_change_to;
    }
  }
}