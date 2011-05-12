using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public interface MemberTargetRegistry
  {
    MemberTarget get_member_target_for(MemberInfo member);
  }
}