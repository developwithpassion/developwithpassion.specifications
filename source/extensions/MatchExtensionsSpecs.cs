using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.core;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.extensions
{
  [Subject(typeof(MatchExtensions))]
  public class MatchExtensionsSpecs
  {
    public abstract class concern : use_engine<RhinoFakeEngine>.observe
    {
    }

    public class when_negating_a_match : concern
    {
      Establish c = () =>
      {
        original = fake.an<IMatchAnItem<int>>();
      };

      It should_return_a_negating_matcher_with_access_to_the_original_matcher = () =>
        original.not().should().be_an<NegatingMatcher<int>>().to_negate.ShouldEqual(original);

      static IMatchAnItem<int> original;
    }
  }
}