using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public interface FieldSwitcherFactory
  {
    ISwapValues create_to_target(MemberInfo member);
  }
}