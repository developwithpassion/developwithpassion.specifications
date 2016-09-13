using System;

namespace developwithpassion.specifications.assertions.core
{
  public interface IProvideAccessToAssertions<out ValueToAssertAgainst>
  {
    ValueToAssertAgainst value { get; }
    void run(Action<ValueToAssertAgainst> action);
    Result run<Result>(Func<ValueToAssertAgainst, Result> action);
  }
}