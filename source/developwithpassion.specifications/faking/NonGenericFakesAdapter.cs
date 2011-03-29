using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.specifications.core;

namespace developwithpassion.specifications.faking
{
    public class NonGenericFakesAdapter : IMarshalNonGenericFakeResolutionToAGenericResolution
    {
        IManageFakes fake_accessor;
        Func<Type, MethodInfo> method_factory;

        public NonGenericFakesAdapter(IManageFakes fake_accessor)
        {
            this.fake_accessor = fake_accessor;
            this.method_factory = this.create_method_factory();
        }

        Func<Type, MethodInfo> create_method_factory()
        {
            Expression<Func<IManageFakes, object>> pointer = x => x.the<object>();
            var target_method = ((MethodCallExpression) pointer.Body).Method.GetGenericMethodDefinition();
            return x => target_method.MakeGenericMethod(x);
        }

        public object resolve(Type item)
        {
            return this.method_factory.Invoke(item).Invoke(this.fake_accessor, new object[0]);
        }
    }
}