using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.NSubstitute;

namespace developwithpassion.specifications.nsubstitue
{
    public abstract class Observes : Observes<NSubstituteEngine>
    {
    }

    public class Observes<Contract, Class> : CoreObserves<Contract, Class, NSubstituteEngine>
        where Class : class, Contract
        where Contract : class
    {
    }

    public class Observes<Class> : CoreObserves<Class, Class,NSubstituteEngine> where Class : class
    {
    }
}