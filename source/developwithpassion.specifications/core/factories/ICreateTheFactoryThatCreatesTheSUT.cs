using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheFactoryThatCreatesTheSUT
    {
        ICreateAndManageDependenciesFor<SUT> create<SUT>(IResolveADependencyForTheSUT fake_resolution,IManageFakes manage_fakes);
    }
}