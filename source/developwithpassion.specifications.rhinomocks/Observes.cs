using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.Rhinomocks;

namespace developwithpassion.specifications.rhinomocks
{
    public abstract class Observes : StaticObservations<RhinoFakeEngine>
    {
    }

    public class Observes<Contract, Class> : CoreObserves<Contract, Class, RhinoFakeEngine>
        where Class : class, Contract
        where Contract : class
    {
    }

    public class Observes<Class> : CoreObserves<Class, Class,RhinoFakeEngine> where Class : class
    {
    }
}