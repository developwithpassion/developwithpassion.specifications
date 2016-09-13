using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using Machine.Fakes;
using Machine.Specifications;

namespace developwithpassion.specifications.observations
{
  public abstract class InstanceObservations<Contract, Class, Engine> : CoreObservations<Contract, Class, Engine>
    where Contract : class where Class : class, Contract where Engine : IFakeEngine, new()
  {
    protected static Contract sut;

    Because base_because = () =>
    {
      sut = controller.run_setup();
    };

    protected static Class concrete_sut
    {
      get { return sut.downcast_to<Class>(); }
    }

    protected static IProvideDependencies depends
    {
      get { return controller.depends; }
    }

    protected static SUTFactory<Class> sut_factory
    {
      get { return controller.sut_factory; }
    }

    protected static IConfigureTheSut<Class> sut_setup
    {
      get { return controller.test_state; }
    }
  }
}