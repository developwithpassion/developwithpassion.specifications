using System;
using System.Reflection;
using Machine.Specifications;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(FieldMemberAccessor))]
    public class FieldMemberAccessorSpecs
    {
        public class TheItem
        {
            public static string static_value = "lah";
        }

        public class concern : Observes<MemberAccessor, FieldMemberAccessor>
        {
            Establish c = delegate
            {
                the_target_type = typeof(TheItem);
                member = the_target_type.GetField("static_value");
                depends.on(member);
            };

            protected static FieldInfo member;
            protected static Type the_target_type;
        }

        public class when_getting_Its_value : concern
        {
            Because b = () =>
                result = sut.get_value(the_target_type);

            It should_get_the_value_of_the_field = () =>
                result.ShouldEqual(TheItem.static_value);

            protected static object result;
        }

        public class when_gettings_its_accessor_type:concern
        {
            It should_return_the_type_of_its_field = () =>
                sut.accessor_type.ShouldEqual(member.FieldType);
        }

        public class when_setting_Its_value : concern
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
    }
}