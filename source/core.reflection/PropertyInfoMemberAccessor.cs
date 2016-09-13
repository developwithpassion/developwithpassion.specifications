using System;
using System.Reflection;

namespace developwithpassion.specifications.core.reflection
{
  public class PropertyInfoMemberAccessor : MemberAccessor
  {
    PropertyInfo member;

    public PropertyInfoMemberAccessor(PropertyInfo member)
    {
      this.member = member;
    }

    public Type accessor_type
    {
      get { return member.PropertyType; }
    }

    public Type declaring_type
    {
      get { return member.DeclaringType; }
    }

    public void change_value_to(object target, object new_value)
    {
      if (member.CanWrite) this.member.SetValue(target, new_value, null);
    }

    public string name
    {
      get { return member.Name; }
    }

    public object get_value(object target)
    {
      return this.member.GetValue(target, null);
    }
  }
}