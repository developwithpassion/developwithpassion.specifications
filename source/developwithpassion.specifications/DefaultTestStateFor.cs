using System;
using System.Collections.Generic;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications
{
    public class DefaultTestStateFor<SUT> : TestStateFor<SUT> where SUT : class
    {
        SUT sut;
        ICreateThe<SUT> factory;
        IList<SetupTearDownPair> setup_tear_down_pairs;
        IList<SUTContextSetup<SUT>> sut_context_behaviours;

        public DefaultTestStateFor(ICreateThe<SUT> factory, IList<SetupTearDownPair> behaviours,
                                   IList<SUTContextSetup<SUT>> sut_context_behaviours)
        {
            this.factory = factory;
            this.setup_tear_down_pairs = behaviours;
            this.sut_context_behaviours = sut_context_behaviours;
        }

        public void add_setup_teardown_pair(SetupTearDownPair setup_tear_down_pair)
        {
            this.setup_tear_down_pairs.Add(setup_tear_down_pair);
        }

        public void add_setup_teardown_pair(Action context, Action teardown)
        {
            this.add_setup_teardown_pair(new SetupTearDownPair(context, teardown));
        }

        void build_sut()
        {
            this.sut = this.factory.create();
        }

        public void run(SUTContextSetup<SUT> action)
        {
            this.sut_context_behaviours.Add(action);
        }

        public SUT run_setup()
        {
            this.run_startup_pipeline();
            this.build_sut();
            this.run_startup_actions_that_require_sut();
            return this.sut;
        }

        void run_startup_actions_that_require_sut()
        {
            this.sut_context_behaviours.each(action => action(sut));
        }

        void run_startup_pipeline()
        {
            this.setup_tear_down_pairs.each(x => x.start());
        }

        public void run_tear_down()
        {
            this.setup_tear_down_pairs.each(x => x.finish());
        }
    }
}