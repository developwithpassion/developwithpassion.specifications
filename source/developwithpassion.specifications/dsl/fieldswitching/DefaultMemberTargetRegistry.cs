using System;
using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public class DefaultMemberTargetRegistry : MemberTargetRegistry
    {
        public MemberTarget get_member_target_for(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Field)
            {
                return new FieldMemberTarget(member);
            }
            if (member.MemberType != MemberTypes.Property)
            {
                throw new ArgumentException(string.Format("Unable to handle the request member type", new object[0]));
            }
            return new PropertyInfoMemberTarget(member);
        }
    }
}