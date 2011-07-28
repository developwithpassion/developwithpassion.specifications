using System;
using System.Reflection;
using Machine.Specifications;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(PropertyInfoMemberAccessor))]
    public class PropertyInfoMemberAccessorSpecs
    {
        public class concern : Observes<MemberAccessor, PropertyInfoMemberAccessor>
        {
            Establish c = delegate
            {
                original_value = "original";
                PropertyInfoTargetItem.static_value = original_value;
                the_target_type = typeof(PropertyInfoTargetItem);
                member = the_target_type.GetProperty("static_value");
                member.ShouldNotBeNull();
                depends.on(member);
            };

            protected static PropertyInfo member;
            protected static string original_value;
            protected static Type the_target_type;
        }

        public class PropertyInfoTargetItem
        {
            public static string static_value { get; set; }
        }

        public class when_gettings_its_accessor_type : concern
        {
            It should_return_the_type_of_its_property = () =>
                sut.accessor_type.ShouldEqual(member.PropertyType);
                 
        }
        public class when_getting_its_value : concern
        {
            Because b = () =>
                result = sut.get_value(the_target_type);

            It should_get_the_value_of_the_field = () =>
                result.ShouldEqual(PropertyInfoTargetItem.static_value);

            protected static object result;
        }

        [Subject(typeof(PropertyInfoMemberAccessor))]
        public class when_setting_its_value : concern
        {
            Establish c = () =>
                value_to_change_to = "blasfsfd";

            Because b = () =>
                sut.change_value_to(the_target_type, value_to_change_to);

            It should_change_the_value_of_the_field = () =>
                PropertyInfoTargetItem.static_value.ShouldEqual(value_to_change_to);

            protected static string value_to_change_to;
        }
    }
}