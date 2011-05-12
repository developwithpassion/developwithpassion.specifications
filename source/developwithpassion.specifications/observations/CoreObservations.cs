using developwithpassion.specifications.core;
using developwithpassion.specifications.core.factories;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Factories;

namespace developwithpassion.specifications.observations
{
  public abstract class CoreObservations<Contract, Class, Engine> where Contract : class
                                                                  where Class : class, Contract
                                                                  where Engine : IFakeEngine, new()
  {
    internal static ObservationController<Class> controller;

    static CoreObservations()
    {
      ContextFactory.ChangeAllowedNumberOfBecauseBlocksTo(5);
    }

    Establish c = () => { controller = Factories.main_controller.create_main_controller<Class, Engine>(); };

    Cleanup core_cleanup = () =>
    {
      controller.run_tear_down();
    };

    protected static ICreateFakes fake
    {
      get { return controller.fake; }
    }

    protected static IConfigureSetupPairs pipeline
    {
      get { return controller.test_state; }
    }

    protected static IConfigureSpecifications spec
    {
      get { return controller; }
    }
  }
}