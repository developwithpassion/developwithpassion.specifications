using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
  public interface ICreateTheNonCtorDependencyVisitor
  {
    IUpdateNonCtorDependenciesOnAnItem create(IManageTheDependenciesForASUT dependency_registry);
  }

  public class NonCtorDependencyVisitorFactory : ICreateTheNonCtorDependencyVisitor
  {
    public IUpdateNonCtorDependenciesOnAnItem create(IManageTheDependenciesForASUT dependency_registry)
    {
      return new NonCtorDependencySetter(dependency_registry);
    }
  }
}