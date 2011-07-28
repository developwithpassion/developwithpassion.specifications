using System;
using System.Reflection;

namespace developwithpassion.specifications.core.reflection
{
    public class FieldMemberAccessor : MemberAccessor
    {
        FieldInfo member;

        public FieldMemberAccessor(FieldInfo member)
        {
            this.member = member;
        }

        public Type accessor_type
        {
            get { return member.FieldType; }
        }

        public string name
        {
            get { return member.Name; }
        }

        public void change_value_to(object target,object new_value)
        {
            this.member.SetValue(target, new_value);
        }

        public Type declaring_type
        {
            get { return member.DeclaringType;}
        }

        public object get_value(object target)
        {
            return this.member.GetValue(target);
        }
    }
}