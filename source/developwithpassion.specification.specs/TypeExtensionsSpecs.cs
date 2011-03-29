using System.Collections.Generic;
using System.Data;
using System.Reflection;
using developwithpassion.specifications.extensions;
using Machine.Specifications;
using System.Linq;

namespace developwithpassion.specification.specs
{
    public class TypeExtensionsSpecs
    {
        public delegate void SomeThing();

        public class SomethingWithParameterfulConstructors
        {
            public SomeThing first_thing;
            public SomeThing second_thing;
            public int third_thing;
            public SomethingWithParameterfulConstructors(IDbConnection connection) : this(connection, null)
            {
            }

            public SomethingWithParameterfulConstructors(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }

            public IDbCommand command { get; set; }
            public IDbConnection connection { get; set; }
        }

        [Subject(typeof(TypeExtensions))]
        public class when_a_generic_type_is_told_to_return_its_proper_name
        {
            Because b = () =>
                result = typeof(List<int>).proper_name();

            It should_return_a_name_that_has_its_generic_type_arguments_expanded = () =>
                result.ShouldEqual("List`1<System.Int32>");

            protected static string result;
        }

        [Subject(typeof(TypeExtensions))]
        public class when_a_type_is_told_to_find_its_greediest_constructor
        {
            Because b = () =>
                result = typeof(SomethingWithParameterfulConstructors).greediest_constructor();

            It should_return_the_constructor_that_takes_the_most_arguments = () =>
                result.GetParameters().Count().ShouldEqual(2);

            protected static ConstructorInfo result;
        }

        [Subject(typeof(TypeExtensions))]
        public class when_told_to_get_a_list_of_fields_of_certain_type
        {
            Because b = () =>
                result =
                    typeof(SomethingWithParameterfulConstructors).all_fields_of<SomeThing>(
                        BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public |
                            BindingFlags.Instance);

            It should_only_return_the_fields_that_match_the_expected_type = () =>
                result.Count().ShouldEqual(2);

            protected static IEnumerable<FieldInfo> result;
        }
    }
}