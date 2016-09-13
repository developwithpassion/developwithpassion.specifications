using developwithpassion.specifications.observations;
using Machine.Fakes;

namespace developwithpassion.specifications
{
  public abstract class use_engine<FakeEngine> where FakeEngine : IFakeEngine, new()
  {
    public class observe : StaticObservations<FakeEngine>
    {
    }

    public class observe<Contract, Class> : InstanceObservations<Contract, Class, FakeEngine>
      where Class : class, Contract
      where Contract : class
    {
    }

    public class observe<Class> : InstanceObservations<Class, Class, FakeEngine> where Class : class
    {
    }
  }
}