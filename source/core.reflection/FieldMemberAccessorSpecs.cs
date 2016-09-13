using System;
using System.Reflection;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.core.reflection
{
  [Subject(typeof(FieldMemberAccessor))]
  public class FieldMemberAccessorSpecs
  {
    public class TheItem
    {
      public static string static_value = "lah";
      public static readonly string read_only_value = "lah";
    }

    public class concern : use_engine<RhinoFakeEngine>.observe<MemberAccessor, FieldMemberAccessor>
    {
      Establish c = delegate
      {
        the_target_type = typeof(TheItem);
        writable_member = the_target_type.GetField("static_value");
        depends.on(writable_member);
      };

      protected static FieldInfo writable_member;
      protected static Type the_target_type;
    }

    public class when_getting_its_value : concern
    {
      Because b = () =>
        result = sut.get_value(the_target_type);

      It should_get_the_value_of_the_field = () =>
        result.ShouldEqual(TheItem.static_value);

      protected static object result;
    }

    public class when_gettings_its_accessor_type : concern
    {
      It should_return_the_type_of_its_field = () =>
        sut.accessor_type.ShouldEqual(writable_member.FieldType);
    }

    public class when_setting_Its_value : concern
    {
      public class and_the_field_is_not_readonly
      {
        Establish c = () =>
        {
          original_value = TheItem.static_value;
          value_to_change_to = "testing";
        };

        Because b = () =>
          sut.change_value_to(the_target_type, value_to_change_to);

        It should_change_the_value_of_the_field = () =>
          TheItem.static_value.ShouldEqual(value_to_change_to);

        Cleanup cleanup = () =>
          TheItem.static_value = original_value;

        protected static string original_value;
        static string value_to_change_to;
      }

      public class and_the_field_is_readonly
      {
        Establish c = () =>
        {
          original_value = TheItem.read_only_value;
          non_writable_member = the_target_type.GetField("read_only_value");
          depends.on(non_writable_member);
          value_to_change_to = "testing";
        };

        Because b = () =>
          sut.change_value_to(the_target_type, value_to_change_to);

        It should_change_the_value_of_the_field = () =>
          TheItem.read_only_value.ShouldEqual(value_to_change_to);

        Cleanup cleanup = () =>
          sut.change_value_to(the_target_type, original_value);

        protected static string original_value;
        static string value_to_change_to;
        static FieldInfo non_writable_member;
      }
    }
  }
}