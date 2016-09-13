using developwithpassion.specifications.core;
using developwithpassion.specifications.faking;

namespace developwithpassion.specifications
{
  public interface ObservationController<Class>
    : IConfigureSpecifications
    where Class : class
  {
    Class run_setup();
    void run_tear_down();

    ICreateFakes fake { get; }
    IProvideDependencies depends { get; }
    SUTFactory<Class> sut_factory { get; }
    IMaintainStateForATest<Class> test_state { get; }
  }
}