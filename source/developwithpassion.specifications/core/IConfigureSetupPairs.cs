using System;

namespace developwithpassion.specifications.core
{
    public interface IConfigureSetupPairs
    {
        void add_setup_teardown_pair(SetupTearDownPair setup_tear_down_pair);
        void add_setup_teardown_pair(Action context, Action teardown);
    }
}