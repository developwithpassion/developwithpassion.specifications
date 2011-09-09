using System;
using System.Linq.Expressions;
using Machine.Fakes;

namespace developwithpassion.specifications.extensions
{
    public static class FakeExtensions
    {
        public static void never_received<Fake>(this Fake item, Expression<Action<Fake>> behaviour) where Fake : class
        {
            item.WasNotToldTo(behaviour);
        }

        public static IMethodCallOccurance received<Fake>(this Fake item, Expression<Action<Fake>> behaviour)
            where Fake : class
        {
            return item.WasToldTo(behaviour);
        }

        public static IQueryOptions<TReturnValue> setup<TFake, TReturnValue>(this TFake fake,
                                                                             Expression<Func<TFake, TReturnValue>> func)
            where TFake : class
        {
            return fake.WhenToldTo(func);
        }

        public static ICommandOptions setup<TFake>(this TFake fake,
                                                                             Expression<Action<TFake>> action) where TFake:class
        {
            return fake.WhenToldTo(action);
        }
    }
}