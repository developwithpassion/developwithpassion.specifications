using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public interface FieldSwitcherFactory
    {
        FieldSwitcher create_to_target(MemberInfo member);
    }
}