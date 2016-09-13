using System;
using System.Linq;
using System.Linq.Expressions;

namespace developwithpassion.specifications.faking
{
  public class FakeDelegateFactory : ICreateFakeDelegates
  {
    public object generate_delegate_for(Type delegate_type)
    {
      var method = delegate_type.GetMethod("Invoke");
      var parameters = method.GetParameters().Select(x => Expression.Parameter(x.ParameterType)).ToList();
      var dynamic_method = Expression.Lambda(delegate_type, create_method_body_based_on(method.ReturnType), parameters);
      return dynamic_method.Compile();
    }

    Expression create_method_body_based_on(Type return_type)
    {
      if (return_type == typeof(void)) return Expression.New(typeof(object));
      return Expression.Default(return_type);
    }
  }
}