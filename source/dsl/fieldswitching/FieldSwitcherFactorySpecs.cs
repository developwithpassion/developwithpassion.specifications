using System.Reflection;
using developwithpassion.specifications.assertions.interactions;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public class FieldSwitcherFactorySpecs
  {
    public class Item
    {
      public static string static_value { get; set; }
    }

    public class concern : use_engine<RhinoFakeEngine>.observe<FieldSwitcherFactory, DefaultFieldSwitcherFactory>
    {
      Establish c = () =>
      {
        member = typeof(Item).GetProperty("static_value");
        registry = depends.on<ICreateAnAccessorForAMember>();
        member_accessor = fake.an<MemberAccessor>();
        member_accessor.setup(x => x.get_value(typeof(Item))).Return("Blah");
        registry.setup(x => x.create_accessor_for(member)).Return(member_accessor);
      };

      protected static PropertyInfo member;
      protected static MemberAccessor member_accessor;
      protected static ICreateAnAccessorForAMember registry;
    }

    [Subject(typeof(DefaultFieldSwitcherFactory))]
    public class when_creating_a_field_switcher : concern
    {
      Because b = () =>
        result = sut.create_to_target(member);

      static ISwapValues result;

      It should_use_the_member_target_registry_to_create_a_target_to_target_the_member_type = () =>
        result.ShouldBeOfExactType<MemberTargetValueSwapper>();
    }
  }
}