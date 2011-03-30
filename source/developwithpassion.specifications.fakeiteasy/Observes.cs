using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.FakeItEasy;

namespace developwithpassion.specifications.fakeiteasy
{
    public abstract class Observes : StaticObservations<FakeItEasyEngine>
    {
    }

    public class Observes<Contract, Class> : CoreObserves<Contract, Class, FakeItEasyEngine>
        where Class : class, Contract
        where Contract : class
    {
    }

    public class Observes<Class> : CoreObserves<Class, Class,FakeItEasyEngine> where Class : class
    {
    }
}