using System;
using System.Collections.Generic;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class SUTDependencyResolverSpecs
    {
        public abstract class concern : Observes<IResolveADependencyForTheSUT,
                                            SUTDependencyResolver>
        {
            protected static IManageFakes accessor;

            Establish c = () =>
                accessor = depends.on<IManageFakes>();
        }

        public class TheSUT
        {
        }

        public class TypeToResolve
        {
        }

        [Subject(typeof(SUTDependencyResolver))]
        public class when_resolving_a_reference_type : concern
        {
            Establish c = () =>
            {
                type_to_resolve = fake.an<TypeToResolve>();
                accessor.setup(x => x.the<TypeToResolve>()).Return(type_to_resolve);
            };

            Because b = () =>
                result = sut.resolve(typeof(TypeToResolve));

            It should_dispatch_the_correct_generic_call_to_the_fake_accessor = () =>
                result.ShouldEqual(type_to_resolve);

            static object result;
            static TypeToResolve type_to_resolve;
        }

        [Subject(typeof(SUTDependencyResolver))]
        public class when_resolving_a_value_type : concern
        {
            Establish c = () =>
            {
                pairs = new Dictionary<Type, object>
                {
                    {typeof(DateTime), default(DateTime)},
                    {typeof(int), default(int)},
                    {typeof(long), default(long)},
                    {typeof(decimal), default(decimal)},
                    {typeof(double), default(double)},
                    {typeof(bool), default(bool)},
                    {typeof(SomeType),default(SomeType)}
                }; 
            };

            It should_return_a_new_instance_of_the_requested_value_type = () =>
                pairs.each(pair => sut.resolve(pair.Key).ShouldEqual(pair.Value));

            static IDictionary<Type,object> pairs;
        }

        [Subject(typeof(SUTDependencyResolver))]
        public class when_resolving_a_string : concern
        {

            Because b = () =>
                result = sut.resolve(typeof(string));

            It should_return_an_empty_string = () =>
                result.ShouldEqual(string.Empty);

            static IDictionary<Type,object> pairs;
            static object result;
        }

        public struct SomeType
        {
            
        }
    }
}