using System.Collections.Generic;
using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public class TestStateFactory : ICreateTheTestState
    {
        public TestStateFor<SUT> create_for<SUT>(ICreateAndManageDependenciesFor<SUT> sut_factory) where SUT : class
        {
            return new DefaultTestStateFor<SUT>(sut_factory, new List<ObservationPair>(),
                                                new List<SUTContextSetup<SUT>>());
        }
    }
}