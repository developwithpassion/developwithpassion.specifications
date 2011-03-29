using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using developwithpassion.specifications.dsl.fieldswitching;

namespace developwithpassion.specifications.core
{
    public interface IConfigureSpecifications
    {
        void catch_exception(Action behaviour_to_trigger);
        void catch_exception<T>(Func<IEnumerable<T>> behaviour);
        ChangeValueInPipeline change(Expression<Func<object>> expression);
        Exception exception_thrown { get; }
    }
}