using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
    public class FieldMemberTarget : MemberTarget
    {
        FieldInfo member;

        public FieldMemberTarget(MemberInfo member_info)
        {
            this.member = member_info.DeclaringType.GetField(member_info.Name);
        }

        public void change_value_to(object new_value)
        {
            this.member.SetValue(this.member.DeclaringType, new_value);
        }

        public object get_value()
        {
            return this.member.GetValue(this.member.DeclaringType);
        }
    }
}