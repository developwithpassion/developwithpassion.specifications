using Machine.Fakes;

namespace developwithpassion.specifications.observations
{
  public abstract class CoreObserves<Engine> : StaticObservations<Engine> where Engine : IFakeEngine, new()
  {
  }

  public abstract class CoreObserves<SystemUnderTest, Engine> :
    InstanceObservations<SystemUnderTest, SystemUnderTest, Engine> where SystemUnderTest : class
                                                                   where Engine : IFakeEngine, new()
  {
  }

  public abstract class CoreObserves<Contract, Class, Engine> : InstanceObservations<Contract, Class, Engine>
    where Contract : class where Class : class, Contract where Engine : IFakeEngine, new()
  {
  }
}