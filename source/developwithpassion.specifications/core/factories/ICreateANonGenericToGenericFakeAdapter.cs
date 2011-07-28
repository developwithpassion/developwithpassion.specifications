using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateANonGenericToGenericFakeAdapter
    {
        IResolveADependencyForTheSUT create(IManageFakes fakes_gateway);
    }

    public class FakesAdapterFactory : ICreateANonGenericToGenericFakeAdapter
    {
        public IResolveADependencyForTheSUT create(IManageFakes fakes_gateway)
        {
            return new SUTDependencyResolver(fakes_gateway,
                                             new FakeDelegateFactory());
        }
    }
}