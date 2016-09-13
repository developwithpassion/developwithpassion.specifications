using developwithpassion.specifications.assertions.interactions;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace developwithpassion.specifications.core
{
  [Subject(typeof(NegatingMatcher<>))]
  public class NegatingMatcherSpecs
  {
    public abstract class concern : use_engine<RhinoFakeEngine>.observe<NegatingMatcher<int>>
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