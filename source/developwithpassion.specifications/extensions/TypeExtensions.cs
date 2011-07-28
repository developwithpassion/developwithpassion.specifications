using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using developwithpassion.specifications.core.reflection;
using developwithpassion.specifications.dsl.fieldswitching;

namespace developwithpassion.specifications.extensions
{
    public static class TypeExtensions
    {
        public const string generic_argument_type_format = "<{0}>";

        public static IEnumerable<FieldInfo> all_fields_of<FieldType>(this Type type, BindingFlags flags)
        {
            return type.GetFields(flags).Where(x => x.FieldType == typeof(FieldType));
        }

        public static ConstructorInfo greediest_constructor(this Type type)
        {
            return type.GetConstructors().OrderByDescending(x => x.GetParameters().Count()).First();
        }

        public static string proper_name(this Type type)
        {
            var message = new StringBuilder(type.Name);
            if (type.IsGenericType)
            {
                type.GetGenericArguments().each(x => message.AppendFormat("<{0}>", x));
            }
            return message.ToString();
        }

        public static IEnumerable<MemberAccessor> all_instance_accessors(this Type type)
        {
            var registry = new MemberAccessorFactory();
            var flags = BindingFlags.Instance | BindingFlags.Public;
            foreach (var member in type.GetFields(flags))
                yield return registry.create_accessor_for(member);
            foreach (var member in type.GetProperties(flags))
                yield return registry.create_accessor_for(member);

        }
    }
}