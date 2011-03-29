using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheTestState
    {
        TestStateFor<SUT> create_for<SUT>(ICreateAndManageDependenciesFor<SUT> sut_factory) where SUT : class;
    }
}