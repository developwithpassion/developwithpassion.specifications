using System.Reflection;
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

        public abstract class concern : Observes<FieldSwitcherFactory, DefaultFieldSwitcherFactory>
        {
            Establish c = () =>
            {
                member = typeof(Item).GetProperty("static_value");
                registry = depends.on<MemberTargetRegistry>();
                member_target = fake.an<MemberTarget>();
                member_target.setup(x => x.get_value()).Return("Blah");
                registry.setup(x => x.get_member_target_for(member)).Return(member_target);
            };

            protected static PropertyInfo member;
            protected static MemberTarget member_target;
            protected static MemberTargetRegistry registry;
        }

        [Subject(typeof(DefaultFieldSwitcherFactory))]
        public class when_creating_a_field_switcher : concern
        {
            Because b = () =>
                result = sut.create_to_target(member);

            static FieldSwitcher result;

            It should_use_the_member_target_registry_to_create_a_target_to_target_the_member_type = () =>
                result.ShouldBeOfType<DefaultFieldSwitcher>();
        }
    }
}