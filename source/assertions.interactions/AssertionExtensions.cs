using System;
using System.Linq.Expressions;
using developwithpassion.specifications.assertions.core;
using Machine.Fakes;

namespace developwithpassion.specifications.assertions.interactions
{
  public static class FakeExtensions
  {
    public static void never_received<Fake>(this IProvideAccessToAssertions<Fake> extension_point, Expression<Action<Fake>> behaviour) where Fake : class
    {
      extension_point.run(x => x.WasNotToldTo(behaviour));
    }

    public static IMethodCallOccurrence received<Fake>(this IProvideAccessToAssertions<Fake> extension_point, Expression<Action<Fake>> behaviour)
      where Fake : class
    {
      return extension_point.run(x => x.WasToldTo(behaviour));
    }

    public static IQueryOptions<TReturnValue> setup<TFake, TReturnValue>(this TFake fake,
      Expression<Func<TFake, TReturnValue>> func)
      where TFake : class
    {
      return fake.WhenToldTo(func);
    }

    public static ICommandOptions setup<TFake>(this TFake fake,
      Expression<Action<TFake>> action) where TFake : class
    {
      return fake.WhenToldTo(action);
    }
  }
}
