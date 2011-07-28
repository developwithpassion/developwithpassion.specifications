using System.Reflection;
using developwithpassion.specifications.core.reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public interface IFindAccessorsForMembers
    {
        MemberAccessor get_accessor_for(MemberInfo member);
    }
}