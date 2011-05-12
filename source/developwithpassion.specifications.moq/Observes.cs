using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.Moq;

namespace developwithpassion.specifications.moq
{
  public abstract class Observes : StaticObservations<MoqFakeEngine>
  {
  }

  public class Observes<Contract, Class> : CoreObserves<Contract, Class, MoqFakeEngine>
    where Class : class, Contract
    where Contract : class
  {
  }

  public class Observes<Class> : CoreObserves<Class, Class, MoqFakeEngine> where Class : class
  {
  }
}