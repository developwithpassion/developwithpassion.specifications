using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheDependencyRegistry
    {
        IManageTheDependenciesForASUT create<SUT>(IManageFakes fakes_gateway,
                                             IResolveADependencyForTheSUT dependency_resolver);
    }
    public class DependencyRegistryFactory:ICreateTheDependencyRegistry
    {
        public IManageTheDependenciesForASUT create<SUT>(IManageFakes fakes_gateway, IResolveADependencyForTheSUT dependency_resolver)
        {
            return new DependenciesRegistry<SUT>(dependency_resolver, fakes_gateway);
        }
    }
}