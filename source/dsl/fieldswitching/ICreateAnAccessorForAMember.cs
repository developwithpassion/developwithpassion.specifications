using System;
using System.Collections.Generic;
using System.Reflection;
using developwithpassion.specifications.core.reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public interface ICreateAnAccessorForAMember
  {
    MemberAccessor create_accessor_for(MemberInfo member);
  }

  public class MemberAccessorFactory : ICreateAnAccessorForAMember
  {
    IDictionary<MemberTypes, Func<MemberInfo, MemberAccessor>> accessor_factories = new Dictionary<MemberTypes,
      Func<MemberInfo, MemberAccessor>>
    {
      {MemberTypes.Field, member => new FieldMemberAccessor((FieldInfo) member)},
      {MemberTypes.Property, member => new PropertyInfoMemberAccessor((PropertyInfo) member)}
    };

    public MemberAccessor create_accessor_for(MemberInfo member)
    {
      if (accessor_factories.ContainsKey(member.MemberType)) return accessor_factories[member.MemberType](member);

      throw new ArgumentException("Unable to create an accessor for the requested member type");
    }
  }
}