using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
  public interface ICreateTheTestState
  {
    IMaintainStateForATest<SUT> create_for<SUT>(ICreateAndManageDependenciesFor<SUT> sut_factory) where SUT : class;
  }

  public class TestStateFactory : ICreateTheTestState
  {
    public IMaintainStateForATest<SUT> create_for<SUT>(ICreateAndManageDependenciesFor<SUT> sut_factory) where SUT : class
    {
      return new DefaultTestStateFor<SUT>(sut_factory);
    }
  }
}