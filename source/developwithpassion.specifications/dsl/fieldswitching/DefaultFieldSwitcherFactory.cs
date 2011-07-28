using System.Reflection;
using developwithpassion.specifications.core.reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public class DefaultFieldSwitcherFactory : FieldSwitcherFactory
    {
        ICreateAnAccessorForAMember create_an_accessor_for_a_member;

        public DefaultFieldSwitcherFactory() : this(new MemberAccessorFactory())
        {
        }

        public DefaultFieldSwitcherFactory(ICreateAnAccessorForAMember create_an_accessor_for_a_member)
        {
            this.create_an_accessor_for_a_member = create_an_accessor_for_a_member;
        }

        public ISwapValues create_to_target(MemberInfo member)
        {
            return new MemberTargetValueSwapper(this.create_an_accessor_for_a_member.create_accessor_for(member));
        }
    }

}