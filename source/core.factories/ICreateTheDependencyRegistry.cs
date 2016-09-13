using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
  public interface ICreateTheDependencyRegistry
  {
    IManageTheDependenciesForASUT create(IManageFakes fakes_gateway,
      IResolveADependencyForTheSUT dependency_resolver);
  }

  public class DependencyRegistryFactory : ICreateTheDependencyRegistry
  {
    public IManageTheDependenciesForASUT create(IManageFakes fakes_gateway,
      IResolveADependencyForTheSUT dependency_resolver)
    {
      return new DependenciesRegistry(dependency_resolver, fakes_gateway);
    }
  }
}