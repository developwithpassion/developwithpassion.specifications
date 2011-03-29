using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class FakeResolverSpecs
    {
        public abstract class concern : Observes<IMarshalNonGenericFakeResolutionToAGenericResolution,
                                            NonGenericFakesAdapter>
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

        [Subject(typeof(NonGenericFakesAdapter))]
        public class when_resolving_a_specific_type : concern
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
    }
}