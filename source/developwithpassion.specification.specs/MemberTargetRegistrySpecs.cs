using System.Reflection;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class MemberTargetRegistrySpecs
    {
        public abstract class concern : Observes<MemberTargetRegistry, DefaultMemberTargetRegistry>
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

        [Subject(typeof(DefaultMemberTargetRegistry))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_field : concern
        {
            Because b = () =>
                result = sut.get_member_target_for(field);

            It should_get_a_field_member_target = () =>
                result.ShouldBeAn<FieldMemberTarget>();

            protected static MemberTarget result;
        }

        [Subject(typeof(DefaultMemberTargetRegistry))]
        public class when_getting_a_member_target_for_a_member_that_represents_a_property : concern
        {
            // Fields
            Because b = () =>
                result = sut.get_member_target_for(property);

            It should_get_a_field_member_target = () =>
                result.ShouldBeAn<PropertyInfoMemberTarget>();

            protected static MemberTarget result;
        }
    }
}