using System;
using System.Linq.Expressions;
using System.Reflection;
using developwithpassion.specifications.core;

namespace developwithpassion.specifications.faking
{
    public class SUTDependencyResolver : IResolveADependencyForTheSUT
    {
        IManageFakes fake_accessor;
        ICreateFakeDelegates _fakeDelegateFactory;
        Func<Type, MethodInfo> method_factory;

        public SUTDependencyResolver(IManageFakes fake_accessor, ICreateFakeDelegates _fakeDelegateFactory)
        {
            this.fake_accessor = fake_accessor;
            this._fakeDelegateFactory = _fakeDelegateFactory;
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
            if (item.IsValueType) return Activator.CreateInstance(item);
            if (item == typeof(string)) return string.Empty;
            if (typeof(Delegate).IsAssignableFrom(item)) return _fakeDelegateFactory.generate_delegate_for(item);
            return this.method_factory.Invoke(item).Invoke(this.fake_accessor, new object[0]);
        }
    }
}