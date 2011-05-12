using System;

namespace developwithpassion.specifications
{
  public class ObservationPair
  {
    Action setup_behaviour;
    Action tear_down_behaviour;

    public ObservationPair(Action setup_behaviour, Action tear_down_behaviour)
    {
      this.setup_behaviour = setup_behaviour;
      this.tear_down_behaviour = tear_down_behaviour;
    }

    public void setup()
    {
      this.setup_behaviour();
    }

    public void teardown()
    {
      this.tear_down_behaviour();
    }
  }
}