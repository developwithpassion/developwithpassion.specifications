using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;
using Machine.Specifications;
using developwithpassion.specification.specs.utility;
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

        public class when_visting_an_item_that_inherits : concern
        {
            Establish c = () =>
            {
                item = new ItemThatInherits();
                dependency_registry.setup(x => x.has_been_provided_an(typeof(IDbConnection)))
                    .Return(true);
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection)))
                    .Return(fake.an<IDbConnection>());
            };

            Because b = () =>
                sut.update(item);

            It should_not_attempt_to_update_the_accessors_on_the_parent_class = () =>
            {
                item.connection.ShouldNotBeNull();
                item.the_other().ShouldBeNull();
            };

            static ItemThatInherits item;

            public class ItemThatInherits : List<Assembly>
            {
                public IDbConnection connection;
                IDbConnection other_connection;
                public Expression<Func<int, bool>> Matcher;

                public IDbConnection the_other()
                {
                    return other_connection;
                }
            }
        }

        public class when_visting_an_item_that_has_non_public_accessor : concern
        {
            Establish c = () =>
            {
                item = new ItemToUpdate();
                dependency_registry.setup(x => x.has_been_provided_an(typeof(IDbConnection)))
                    .Return(true);
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection)))
                    .Return(fake.an<IDbConnection>());
            };

            Because b = () =>
                sut.update(item);

            It should_not_attempt_to_update_the_non_public_accessor = () =>
            {
                item.connection.ShouldNotBeNull();
                item.the_other().ShouldBeNull();
            };

            static ItemToUpdate item;
        }

        public class when_the_dependency_manager_throws_an_exception_while_trying_to_get_a_dependency_for_a_accessor :
            concern
        {
            Establish c = () =>
            {
                original_exception = new Exception();
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Throw(original_exception);
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDataAdapter))).Return(fake.an<IDataAdapter>());
                item = new AnItem();
            };

            Because b = () =>
                sut.update(item);

            It should_carry_on_attempting_to_set_other_dependencies = () =>
                item.adapter.ShouldNotBeNull();

            static Exception original_exception;
            static AnItem item;

            public class AnItem
            {
                public IDbConnection connection;
                public IDataAdapter adapter;
                IDbConnection other_connection;

                public IDbConnection the_other()
                {
                    return other_connection;
                }
            }
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
                        original_connection = fake.an<IDbConnection>();
                        item = new ItemToUpdate {connection = original_connection};
                    };

                    Because b = () =>
                        sut.update(item);

                    It should_overwrite_the_value = () =>
                        item.connection.ShouldEqual(the_connection_from_the_registry);

                    static ItemToUpdate item;
                    static IDbConnection the_connection_from_the_registry;
                    static IDbConnection original_connection;
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

        public class integration
        {
            public class concern : Observes
            {
                Establish c = () =>
                {
                    original = item => item.ToLower();
                    dependencies = ObjectFactory.create_dependencies<ItemToUpdate>();
                    sut = ObjectFactory.create_visitor<ItemToUpdate>(dependencies);
                    item_to_update = new ItemWithDelegateFields {Configuration = original};
                };

                protected static IUpdateNonCtorDependenciesOnAnItem sut;
                protected static IManageTheDependenciesForASUT dependencies;
                protected static ItemWithDelegateFields item_to_update;
                protected static Func<string, string> original;
            }

            public class when_updating_an_item_with_public_delegate_fields : concern
            {
                Because b = () =>
                    sut.update(item_to_update);

                public class and_the_fields_have_a_value : when_updating_an_item_with_public_delegate_fields
                {
                    public class and_the_registry_has_no_value_for_that_field : and_the_fields_have_a_value
                    {
                        It should_not_change_their_value = () =>
                            item_to_update.Configuration.ShouldEqual(original);
                    }

                    public class and_the_registry_has_a_value_for_the_field : and_the_fields_have_a_value
                    {
                        Establish c = () =>
                        {
                            new_value = item => item;
                            dependencies.on(new_value);
                        };

                        It should_change_the_value_of_the_delegate_field_to_the_provided_value = () =>
                            item_to_update.Configuration.ShouldEqual(new_value);

                        static Func<string, string> new_value;
                    }
                }

                public class and_the_field_does_not_have_a_value : when_updating_an_item_with_public_delegate_fields
                {
                    Establish c = () =>
                    {
                        item_to_update.Configuration = null;
                    };

                    public class and_the_registry_has_no_value_for_that_field : and_the_fields_have_a_value
                    {
                        It should_change_the_value_to_be_non_null = () =>
                            item_to_update.Configuration.ShouldNotBeNull();
                    }

                    public class and_the_registry_has_a_value_for_the_field : and_the_fields_have_a_value
                    {
                        Establish c = () =>
                        {
                            new_value = item => item;
                            dependencies.on(new_value);
                        };

                        It should_change_the_value_of_the_delegate_field_to_the_provided_value = () =>
                            item_to_update.Configuration.ShouldEqual(new_value);

                        static Func<string, string> new_value;
                    }
                }
            }

            public class ItemWithDelegateFields
            {
                public Func<string, string> Configuration;
            }
        }

        public class ItemToUpdate
        {
            public IDbConnection connection;
            IDbConnection other_connection;

            public IDbConnection the_other()
            {
                return other_connection;
            }
        }
    }
}