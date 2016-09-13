using System;

namespace developwithpassion.specifications.core
{
  public class BlockThat
  {
    public static Result ignores_exceptions<Result>(Func<Result> action, Result failure_return_value)
    {
      Result result = failure_return_value;
      ignores_exceptions(() =>
      {
        result = action();
      });
      return result;
    }

    public static Result ignores_exceptions<Result>(Func<Result> action)
    {
      return ignores_exceptions(action, default(Result));
    }

    public static void ignores_exceptions(Action action)
    {
      try
      {
        action();
      }
      catch
      {
      }
    }
  }
}