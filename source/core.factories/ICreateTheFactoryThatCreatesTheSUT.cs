using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
  public interface ICreateTheFactoryThatCreatesTheSUT
  {
    ICreateAndManageDependenciesFor<SUT> create<SUT>(IManageTheDependenciesForASUT dependency_registry,
      IUpdateNonCtorDependenciesOnAnItem non_ctor_dependency_visitor);
  }

  public class SUTFactoryProvider : ICreateTheFactoryThatCreatesTheSUT
  {
    public ICreateAndManageDependenciesFor<SUT> create<SUT>(IManageTheDependenciesForASUT dependency_registry,
      IUpdateNonCtorDependenciesOnAnItem
        non_ctor_dependency_visitor)
    {
      return new DefaultSUTFactory<SUT>(dependency_registry,
        non_ctor_dependency_visitor);
    }
  }
}