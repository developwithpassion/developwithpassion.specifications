using System.Reflection;
using developwithpassion.specifications.core.reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public class DefaultFieldSwitcherFactory : FieldSwitcherFactory
    {
        IFindAccessorsForMembers find_accessors_for_members;

        public DefaultFieldSwitcherFactory() : this(new MemberAccessorRegistry())
        {
        }

        public DefaultFieldSwitcherFactory(IFindAccessorsForMembers find_accessors_for_members)
        {
            this.find_accessors_for_members = find_accessors_for_members;
        }

        public ISwapValues create_to_target(MemberInfo member)
        {
            return new MemberTargetValueSwapper(this.find_accessors_for_members.get_accessor_for(member));
        }
    }

}