using developwithpassion.specifications.assertions.core;
using Machine.Specifications;

namespace developwithpassion.specifications.assertions.type_specificity
{
  public static class AssertionExtensions
  {
    public static T be_an<T>(this IProvideAccessToAssertions<object> extension_point)
    {
      extension_point.value.ShouldBeOfExactType<T>();
      return (T) extension_point.value;
    }
  }
}