using System.Reflection;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class FieldSwitcherFactorySpecs
    {
        public class Item
        {
            public static string static_value { get; set; }
        }

        public class concern : Observes<FieldSwitcherFactory, DefaultFieldSwitcherFactory>
        {
            Establish c = () =>
            {
                member = typeof(Item).GetProperty("static_value");
                registry = depends.on<IFindAccessorsForMembers>();
                member_accessor = fake.an<MemberAccessor>();
                member_accessor.setup(x => x.get_value(typeof(Item))).Return("Blah");
                registry.setup(x => x.get_accessor_for(member)).Return(member_accessor);
            };

            protected static PropertyInfo member;
            protected static MemberAccessor member_accessor;
            protected static IFindAccessorsForMembers registry;
        }

        [Subject(typeof(DefaultFieldSwitcherFactory))]
        public class when_creating_a_field_switcher : concern
        {
            Because b = () =>
                result = sut.create_to_target(member);

            static ISwapValues result;

            It should_use_the_member_target_registry_to_create_a_target_to_target_the_member_type = () =>
                result.ShouldBeOfType<MemberTargetValueSwapper>();
        }
    }
}