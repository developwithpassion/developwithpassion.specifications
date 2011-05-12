using System.Reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public class PropertyInfoMemberTarget : MemberTarget
  {
    PropertyInfo member;

    public PropertyInfoMemberTarget(MemberInfo member)
    {
      this.member = member.DeclaringType.GetProperty(member.Name);
    }

    public void change_value_to(object new_value)
    {
      this.member.SetValue(this.member.DeclaringType, new_value, new object[0]);
    }

    public object get_value()
    {
      return this.member.GetValue(this.member.DeclaringType, new object[0]);
    }
  }
}