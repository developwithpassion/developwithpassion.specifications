using Machine.Specifications;
using Rhino.Mocks;
using developwithpassion.specifications.core;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;
namespace developwithpassion.specification.specs
{
    [Subject(typeof(NegatingMatcher<>))]
    public class NegatingMatcherSpecs
    {
        public abstract class concern : Observes<NegatingMatcher<int>> 
        {
            Establish c = () =>
            {
                original_matcher = depends.on<IMatchAnItem<int>>();
            };

            protected static IMatchAnItem<int> original_matcher;
        }

        public class when_matching : concern
        {
            Establish c = () =>
            {
                original_matcher.setup(x => x.matches(Arg<int>.Is.Anything)).Return(true);
            };

            It should_return_the_opposite_result_of_its_original_matcher = () =>
                sut.matches(2).ShouldBeFalse();
        }
    }
}
