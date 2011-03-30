using System;
using System.Collections.Generic;
using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public class SUTFactoryProvider : ICreateTheFactoryThatCreatesTheSUT
    {
        public ICreateAndManageDependenciesFor<SUT> create<SUT>(IResolveADependencyForTheSUT fake_resolution,
            IManageFakes manage_fakes)
        {
            return new DefaultSUTFactory<SUT>(new Dictionary<Type, object>(), fake_resolution,manage_fakes);
        }
    }
}