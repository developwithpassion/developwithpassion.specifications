using System.Reflection;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public class MemberAccessorFactorySpecs
  {
    public class concern : use_engine<MoqFakeEngine>.observe<ICreateAnAccessorForAMember, MemberAccessorFactory>
    {
      Establish c = delegate
      {
        property = typeof(Item).GetProperty("static_property");
        field = typeof(Item).GetField("static_value");
      };

      protected static MemberInfo field;
      protected static MemberInfo property;
    }

    public class Item
    {
      public static string static_value = "blah";
      public static string static_property { get; set; }
    }

    [Subject(typeof(MemberAccessorFactory))]
    public class when_getting_a_member_target_for_a_member_that_represents_a_field : concern
    {
      Because b = () =>
        result = sut.create_accessor_for(field);

      It should_get_a_field_member_target = () =>
        result.should().be_an<FieldMemberAccessor>();

      protected static MemberAccessor result;
    }

    [Subject(typeof(MemberAccessorFactory))]
    public class when_getting_a_member_target_for_a_member_that_represents_a_property : concern
    {
      Because b = () =>
        result = sut.create_accessor_for(property);

      It should_get_a_field_member_target = () =>
        result.should().be_an<PropertyInfoMemberAccessor>();

      protected static MemberAccessor result;
    }
  }
}
