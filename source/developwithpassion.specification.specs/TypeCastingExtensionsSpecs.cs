using System;
using System.Collections.Generic;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class TypeCastingExtensionsSpecs
    {
        public class BaseType
        {
        }

        public class OtherType
        {
        }

        public class SubType : BaseType
        {
        }

        [Subject(typeof(TypeCastingExtensions))]
        public class when_a_legitimate_downcast_is_made
        {
            Because b = () =>
                new List<int>().downcast_to<List<int>>();

            It should_not_fail = () => { };
        }

        [Subject(typeof(TypeCastingExtensions))]
        public class when_an_illegal_downcast_is_attempted : Observes
        {
            Because b = () =>
                spec.catch_exception(() => 2.downcast_to<DateTime>());

            It should_throw_an_invalid_cast_exception = () =>
                spec.exception_thrown.ShouldBeAn<InvalidCastException>();
        }

        [Subject(typeof(TypeCastingExtensions))]
        public class when_determining_if_an_object_is_not_an_instance_of_a_specific_type
        {
            It should_make_determination_based_on_whether_the_object_is_assignable_from_the_specific_type = () =>
            {
                new OtherType().is_not_a<BaseType>().ShouldBeTrue();
                new SubType().is_not_a<BaseType>().ShouldBeFalse();
            };
        }
    }
}