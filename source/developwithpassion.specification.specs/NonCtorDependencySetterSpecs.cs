using System.Data;
using System.Data.SqlClient;
using Machine.Specifications;
using developwithpassion.specifications.core.factories;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(NonCtorDependencySetter))]
    public class NonCtorDependencySetterSpecs
    {
        public abstract class concern : Observes
        {
            Establish c = () =>
            {
                dependency_registry = fake.an<IManageTheDependenciesForASUT>();
                sut = new NonCtorDependencySetter(dependency_registry);
            };

            protected static IUpdateNonCtorDependenciesOnAnItem sut;
            protected static IManageTheDependenciesForASUT dependency_registry;
        }

        public class when_visiting_an_item_that_has_public_accessors : concern
        {
            public class and_the_accessors_have_not_been_set : when_visiting_an_item_that_has_public_accessors
            {
                public class and_a_value_has_been_registered_in_the_registry : and_the_accessors_have_not_been_set
                {
                    Establish c = () =>
                    {
                        the_connection_from_the_registry = fake.an<IDbConnection>();
                        dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Return(
                            the_connection_from_the_registry);
                        item = new ItemToUpdate();
                    };

                    Because b = () =>
                        sut.update(item);

                    It should_update_the_fields_with_the_values_from_the_dependency_registry = () =>
                        item.connection.ShouldEqual(the_connection_from_the_registry);

                    static ItemToUpdate item;
                    static IDbConnection the_connection_from_the_registry;

                } 

                public class and_a_value_has_not_been_registered_in_the_registry : and_the_accessors_have_not_been_set
                {
                    Establish c = () =>
                    {
                        the_connection_from_the_registry = fake.an<IDbConnection>();
                        dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Return(
                            the_connection_from_the_registry);
                        item = new ItemToUpdate();
                    };

                    Because b = () =>
                        sut.update(item);

                    It should_update_the_fields_with_the_values_from_the_dependency_registry = () =>
                        item.connection.ShouldEqual(the_connection_from_the_registry);

                    static ItemToUpdate item;
                    static IDbConnection the_connection_from_the_registry;
                }
            }

            public class and_the_accessors_have_been_set : when_visiting_an_item_that_has_public_accessors
            {
                public class and_the_dependencies_were_explicitly_specified_in_the_registry :
                    and_the_accessors_have_been_set
                {
                    Establish c = () =>
                    {
                        the_connection_from_the_registry = fake.an<IDbConnection>();
                        dependency_registry.setup(x => x.has_been_provided_an(typeof(IDbConnection))).Return(true);
                        dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Return(
                            the_connection_from_the_registry);
                        item = new ItemToUpdate {connection = new SqlConnection()};
                    };

                    Because b = () =>
                        sut.update(item);

                    It should_update_the_accessors_with_the_values_from_the_dependency_registry = () =>
                        item.connection.ShouldEqual(the_connection_from_the_registry);

                    static ItemToUpdate item;
                    static IDbConnection the_connection_from_the_registry;
                }

                public class and_the_dependencies_were_not_specified_in_the_registry :
                    and_the_accessors_have_been_set
                {
                    Establish c = () =>
                    {
                        the_connection_from_the_registry = fake.an<IDbConnection>();
                        dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Return(
                            the_connection_from_the_registry);
                        original_connection = fake.an<IDbConnection>();
                        item = new ItemToUpdate {connection = original_connection};
                    };

                    Because b = () =>
                        sut.update(item);

                    It should_not_override_the_original_accessors = () =>
                        item.connection.ShouldEqual(original_connection);

                    static ItemToUpdate item;
                    static IDbConnection the_connection_from_the_registry;
                    static IDbConnection original_connection;
                }
            }
        }

        public class ItemToUpdate
        {
            public IDbConnection connection;
        }
    }
}