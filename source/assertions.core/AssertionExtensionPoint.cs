using System;

namespace developwithpassion.specifications.assertions.core
{
  public class AssertionExtensionPoint<ValueToAssertAgainst> : IProvideAccessToAssertions<ValueToAssertAgainst>
  {
    public ValueToAssertAgainst value { get; }

    public AssertionExtensionPoint(ValueToAssertAgainst value)
    {
      this.value = value;
    }

    public void run(Action<ValueToAssertAgainst> action)
    {
      action(value);
    }

    public Result run<Result>(Func<ValueToAssertAgainst, Result> action)
    {
      return action(value);
    }
  }
}