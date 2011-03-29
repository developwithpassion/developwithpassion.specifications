using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public class DefaultFieldSwitcherFactory : FieldSwitcherFactory
    {
        MemberTargetRegistry member_target_registry;

        public DefaultFieldSwitcherFactory() : this(new DefaultMemberTargetRegistry())
        {
        }

        public DefaultFieldSwitcherFactory(MemberTargetRegistry member_target_registry)
        {
            this.member_target_registry = member_target_registry;
        }

        public ISwapValues create_to_target(MemberInfo member)
        {
            return new MemberTargetValueSwapper(this.member_target_registry.get_member_target_for(member));
        }
    }

}