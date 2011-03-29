using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.FakeItEasy;

namespace developwithpassion.specifications.fakeiteasy
{
    public abstract class Observes : Observes<FakeItEasyEngine>
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