using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheFactoryThatCreatesTheSUT
    {
        ICreateAndManageDependenciesFor<SUT> create<SUT>(IMarshalNonGenericFakeResolutionToAGenericResolution fake_resolution,IManageFakes manage_fakes);
    }
}