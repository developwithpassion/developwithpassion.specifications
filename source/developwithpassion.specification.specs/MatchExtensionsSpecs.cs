using Machine.Specifications;
using developwithpassion.specifications.core;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(MatchExtensions))]
    public class MatchExtensionsSpecs
    {
        public abstract class concern : Observes
        {

        }

        public class when_negating_a_match : concern
        {
            Establish c = () =>
            {
                original = fake.an<IMatchAnItem<int>>();
            };

            It should_return_a_negating_matcher_with_access_to_the_original_matcher = () =>
                original.not().ShouldBeAn<NegatingMatcher<int>>().to_negate.ShouldEqual(original);

            static IMatchAnItem<int> original;
                
        }
    }
}
