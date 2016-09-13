using System;
using System.Collections.Generic;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications
{
  public class DefaultTestStateFor<SUT> : IMaintainStateForATest<SUT> where SUT : class
  {
    SUT sut;
    ICreateThe<SUT> factory;
    public IList<ObservationPair> setup_tear_down_pairs = new List<ObservationPair>();
    public IList<SUTContextSetup<SUT>> sut_context_behaviours = new List<SUTContextSetup<SUT>>();

    public DefaultTestStateFor(ICreateThe<SUT> factory)
    {
      this.factory = factory;
    }

    public void add_setup_teardown_pair(ObservationPair observation_pair)
    {
      this.setup_tear_down_pairs.Add(observation_pair);
    }

    public void add_setup_teardown_pair(Action context, Action teardown)
    {
      this.add_setup_teardown_pair(new ObservationPair(context, teardown));
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
      this.setup_tear_down_pairs.each(x => x.setup());
    }

    public void run_tear_down()
    {
      this.setup_tear_down_pairs.each(x => x.teardown());
    }
  }
}