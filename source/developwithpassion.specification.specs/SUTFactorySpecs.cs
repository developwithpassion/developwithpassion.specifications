using System;
using System.Collections.Generic;
using System.Data;
using Machine.Specifications;
using developwithpassion.specification.specs.utility;
using developwithpassion.specifications.core;
using developwithpassion.specifications.core.factories;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(DefaultSUTFactory<>))]
    public class SUTFactorySpecs
    {
        public class concern : Observes
        {
            Establish base_setup = delegate
            {
                connection = fake.an<IDbConnection>();
                command = fake.an<IDbCommand>();
                non_ctor_dependency_visitor = fake.an<IUpdateNonCtorDependenciesOnAnItem>();
                dependency_registry = fake.an<IManageTheDependenciesForASUT>();
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDbConnection))).Return(connection);
                dependency_registry.setup(x => x.get_dependency_of(typeof(IDbCommand))).Return(command);
            };

            protected static DefaultSUTFactory<ItemToBeCreated> create_sut<ItemToBeCreated>()
            {
                return new DefaultSUTFactory<ItemToBeCreated>(dependency_registry,
                                                              non_ctor_dependency_visitor);
            }

            protected static IDbCommand command;
            protected static IDbConnection connection;
            protected static IManageTheDependenciesForASUT dependency_registry;
            protected static IUpdateNonCtorDependenciesOnAnItem non_ctor_dependency_visitor;
        }

        public class concern_for_type_with_some_non_fakeable_ctor_parameters : concern
        {
            Establish c = delegate
            {
                original_exception = new Exception();
                sut = create_sut<ItemWithNonFakeableCtorParameters>();
            };

            protected static Exception original_exception;
            protected static DefaultSUTFactory<ItemWithNonFakeableCtorParameters> sut;
        }

        public class when_creating_a_type : concern
        {
            public class and_no_errors_occur_during_the_initial_instantiation : when_creating_a_type
            {
                Establish c = () =>
                {
                    sut = create_sut<ItemWithNoCtorParameters>();
                };

                Because b = () =>
                    result = sut.create();

                It should_run_the_non_ctor_dependency_visitor_against_the_created_item = () =>
                    non_ctor_dependency_visitor.received(x => x.update(result));

                static ItemWithNoCtorParameters result;
                static DefaultSUTFactory<ItemWithNoCtorParameters> sut;
            }

            public class ItemWithNoCtorParameters
            {
            }
        }

        public class when_specifying_a_dependency : concern_for_type_with_some_non_fakeable_ctor_parameters
        {
            Establish c = () =>
            {
                dependency_registry.setup(x => x.on<IDbConnection>()).Return(connection);
            };

            Because b = () =>
                result = sut.on<IDbConnection>();

            It should_store_the_dependency_in_the_dependency_registry_and_return_the_item_it_returned = () =>
                result.ShouldEqual(connection);

            static IDbConnection result;
        }

        public class when_creating_a_type_that_has_constructor_parameters_that_cant_be_faked :
            concern_for_type_with_some_non_fakeable_ctor_parameters
        {
            static ItemWithNonFakeableCtorParameters result;

            public class and_arguments_have_not_been_specifically_provided :
                when_creating_a_type_that_has_constructor_parameters_that_cant_be_faked
            {
                Establish c = () =>
                    dependency_registry.setup(x => x.get_dependency_of(typeof(SomeOtherType))).Throw(original_exception);

                Because b = () =>
                    spec.catch_exception(() => sut.create());

                It should_throw_the_original_exception_thrown_by_the_dependency_resolver = () =>
                    spec.exception_thrown.ShouldEqual(original_exception);
            }

            public class and_the_arguments_have_been_specifically_provided :
                when_creating_a_type_that_has_constructor_parameters_that_cant_be_faked
            {
                Establish c = () =>
                {
                    the_item = new SomeOtherType(3);
                    dependency_registry.setup(x => x.get_dependency_of(typeof(SomeOtherType))).Return(the_item);
                };

                Because b = () =>
                    result = sut.create();

                It should_be_able_to_be_created_without_issues = () =>
                    result.other.ShouldEqual(the_item);

                static SomeOtherType the_item;
            }
        }

        public class when_creating_an_item_and_no_constructor_arguments_have_been_provided : concern
        {
            Establish c = () =>
            {
                sut = create_sut<ItemToCreate>();
            };

            Because b = () =>
                result = sut.create();

            static ItemToCreate created_item;
            static ItemToCreate result;

            It should_return_the_item_created_by_the_greediest_constructor = () =>
            {
                result.connection.ShouldEqual(connection);
                result.command.ShouldEqual(command);
            };

            static DefaultSUTFactory<ItemToCreate> sut;
        }

        public class when_creating_an_item_and_a_constructor_argument_was_provided : concern
        {
            public class AnItemToCreate
            {
                int other;
            }

            Establish c = () =>
            {
                sut = create_sut<AnItemToCreate>();
            };

            Because b = () =>
                result = sut.create();

            static AnItemToCreate created_item;
            static AnItemToCreate result;
            static DefaultSUTFactory<AnItemToCreate> sut;

            public class and_the_constructor_for_the_created_item_does_not_require_a_type_of_the_value_provided :
                when_creating_an_item_and_a_constructor_argument_was_provided
            {
                It should_fail_to_create_the_item = () =>
                    sut.create();
            }
        }

        public class when_providing_a_specific_constructor_parameter_for_the_sut : concern
        {
            protected static DefaultSUTFactory<ItemToCreate> sut;

            Establish c = () =>
            {
                sut = create_sut<ItemToCreate>();
                dependency_registry.setup(x => x.on(3)).Return(3);
            };

            Because b = () =>
                result = sut.on(3);

            It should_return_the_value_stored_by_the_dependency_registry = () =>
                result.ShouldEqual(3);

            static int result;
        }

        public class when_provided_a_specific_factory_method : concern
        {
            Establish c = () =>
            {
                sut = create_sut<ItemToCreate>();
                created_item = new ItemToCreate(null, null);
            };

            Because b = () =>
                sut.create_using(() => new ItemToCreate(null, null));

            It should_use_the_factory_method_for_creation = () =>
                sut.create().ShouldNotEqual(created_item);

            protected static ItemToCreate created_item;
            static DefaultSUTFactory<ItemToCreate> sut;
        }

        public class when_providing_a_constructor_argument_for_the_sut_and_an_explicit_factory_has_been_provided :
            concern
        {
            Establish c = () =>
            {
                sut = create_sut<ItemToCreate>();
                sut.create_using(provided_factory);
            };

            Because b = () =>
                sut.on(3);

            It should_not_change_the_value_of_the_sut_factory = () =>
                sut.actual_factory.ShouldEqual(provided_factory);

            protected static Func<ItemToCreate> provided_factory;
            static DefaultSUTFactory<ItemToCreate> sut;
        }

        public class integration : Observes
        {
            protected static IManageFakes manage_fakes;
            protected static ICreateFakeDelegates FakeDelegatesFactory;
            protected static IResolveADependencyForTheSUT dependency_resolver;

            protected static DefaultSUTFactory<ForItem> create_sut<ForItem>() where ForItem : class
            {
                return ObjectFactory.create<ForItem>();
            }

            public class
                when_constructing_a_type_that_has_struct_dependencies_that_have_not_been_provided :
                    integration
            {
                Establish c = () =>
                {
                    sut = create_sut<ItemWithNonFakeableCtorParameters2>();
                };

                Because b = () =>
                    result = sut.create();

                It should_be_able_to_create_it_without_issues = () =>
                    result.ShouldNotBeNull();

                static DefaultSUTFactory<ItemWithNonFakeableCtorParameters2> sut;
                static ItemWithNonFakeableCtorParameters2 result;
            }

            public class
                when_constructing_a_type_that_has_dependencies_specified_as_a_mixture_of_field_properties_and_ctor_args :
                    integration
            {
                Establish c = () =>
                {
                    sut = create_sut<item_with_dependencies_in_ctor_in_fields_and_in_properties>();
                };

                Because b = () =>
                    result = sut.create();

                It should_create_the_item_with_all_dependencies_satisfied = () =>
                {
                    result.ShouldNotBeNull();
                    result.reader.ShouldNotBeNull();
                    result.adapter.ShouldNotBeNull();
                    result.get_the_connection().ShouldNotBeNull();
                };

                static DefaultSUTFactory<item_with_dependencies_in_ctor_in_fields_and_in_properties> sut;
                static item_with_dependencies_in_ctor_in_fields_and_in_properties result;
                static Func<string, string> to_lower;
            }

            public class item_with_dependencies_in_ctor_in_fields_and_in_properties
            {
                IDbConnection connection;
                public IDataReader reader;
                public IDataAdapter adapter { get; set; }
                public Func<string, string> Configuration = item => item.ToUpper();

                public item_with_dependencies_in_ctor_in_fields_and_in_properties(IDbConnection connection)
                {
                    this.connection = connection;
                }

                public IDbConnection get_the_connection()
                {
                    return connection;
                }
            }
        }

        public class ItemWithNonFakeableCtorParameters2
        {
            public IDbConnection connection2 { get; set; }
            public IDbCommand command;
            public IDbConnection connection;
            public int number;
            public int number2;
            public DateTime date;
            public Func<int, bool> condition;

            public ItemWithNonFakeableCtorParameters2(IDbConnection connection, IDbCommand command,
                                                      IDbConnection connection2, int number,
                                                      int number2, DateTime date, Func<int, bool> condition)
            {
                this.connection2 = connection2;
                this.connection = connection;
                this.command = command;
                this.number = number;
                this.number2 = number2;
                this.date = date;
                this.condition = condition;
            }
        }

        public class ItemToCreate
        {
            public IDbCommand command;
            public IDbConnection connection;

            public ItemToCreate(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }
        }

        public class ItemWithNonFakeableCtorParameters
        {
            public IDbCommand command;
            public IDbConnection connection;
            public SomeOtherType other;

            public ItemWithNonFakeableCtorParameters(IDbConnection connection, IDbCommand command, SomeOtherType other)
            {
                this.connection = connection;
                this.command = command;
                this.other = other;
            }
        }

        public class SomeOtherType
        {
            int number;

            public SomeOtherType(int number)
            {
                this.number = number;
            }
        }

        public class NulloVisitor : IUpdateNonCtorDependenciesOnAnItem
        {
            public void update(object item)
            {
            }
        }
    }
}