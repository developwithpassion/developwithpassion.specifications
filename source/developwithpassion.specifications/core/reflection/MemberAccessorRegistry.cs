using System;
using System.Reflection;
using developwithpassion.specifications.dsl.fieldswitching;

namespace developwithpassion.specifications.core.reflection
{
    public class MemberAccessorRegistry : IFindAccessorsForMembers
    {
        public MemberAccessor get_accessor_for(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Field)
            {
                return new FieldMemberAccessor(member.DeclaringType.GetField(member.Name));
            }
            if (member.MemberType == MemberTypes.Property)
            {
                return new PropertyInfoMemberAccessor(member.DeclaringType.GetProperty(member.Name));
            }
            throw new ArgumentException(string.Format("Unable to handle the request member type", new object[0]));
        }
    }
}