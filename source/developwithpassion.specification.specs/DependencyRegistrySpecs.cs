using System;
using System.Collections.Generic;
using System.Data;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;
using developwithpassion.specifications.core;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(DependenciesRegistry<RhinoFakeEngine>))]
    public class DependencyRegistrySpecs
    {
        public abstract class concern : Observes
        {
            Establish c = () =>
            {
                the_connection = fake.an<IDbConnection>();
                dependency_resolver = fake.an<IResolveADependencyForTheSUT>();
                fake_gateway = fake.an<IManageFakes>();
                dependencies = new Dictionary<Type, IDictionary<string, object>>();
                sut = new DependenciesRegistry<RhinoFakeEngine>(dependency_resolver, fake_gateway);
                sut.downcast_to<DependenciesRegistry<RhinoFakeEngine>>().explicit_dependencies = dependencies;
            };

            protected static Dictionary<Type, IDictionary<string, object>> dependencies;
            protected static IManageTheDependenciesForASUT sut;
            protected static IResolveADependencyForTheSUT dependency_resolver;
            protected static IManageFakes fake_gateway;
            protected static Dictionary<string, object> dependencies_by_name_for_idbconnection;
            protected static IDbConnection the_connection;
        }

        public class when_asked_if_it_has_an_explicit_dependency_and_it_has_it_by_name : concern
        {
            Establish c = () =>
            {
                dependencies_by_name_for_idbconnection = new Dictionary<string, object>() { { "", the_connection } };
                dependencies.Add(typeof(IDbConnection), dependencies_by_name_for_idbconnection);
            };

            It should_make_the_decision_based_on_whether_an_explicit_dependency_was_registered = () =>
            {
                sut.has_been_provided_an(typeof(IDbConnection), "connection").ShouldBeTrue();
                sut.has_been_provided_an(typeof(IDbCommand), "command").ShouldBeFalse();
            };

        }

        public class when_getting_a_default_dependency : concern
        {
            public class and_the_dependency_has_been_explicitly_provided_with_a_default : when_getting_a_default_dependency
            {
                private Establish c = () =>
                {
                    dependencies_by_name_for_idbconnection = new Dictionary<string, object>() { { "", the_connection } };
                    dependencies.Add(typeof(IDbConnection), dependencies_by_name_for_idbconnection);
                };

                Because b = () =>
                    result = sut.get_dependency_of(typeof(IDbConnection), "parameter_or_property_name");

                It should_return_the_item_that_was_registered = () =>
                    result.ShouldEqual(the_connection);

                static object result;
            }

            public class and_the_dependency_was_not_explicitly_provided : when_getting_a_default_dependency
            {
                Establish c = () =>
                {
                    dependency_resolver.setup(x => x.resolve(typeof(IDbConnection))).Return(the_connection);
                };

                Because b = () =>
                    result = sut.get_dependency_of(typeof(IDbConnection), "parameter_or_property_name");

                It should_return_the_item_created_by_the_dependency_resolver = () =>
                    result.ShouldEqual(the_connection);

                static object result;
            }
        }
        
        public class when_storing_default_dependencies : concern
        {
            public class and_it_has_not_already_been_stored : when_storing_default_dependencies
            {
                Establish c = () =>
                {
                    the_connection = fake.an<IDbConnection>();
                };

                Because b = () =>
                    result = sut.on(the_connection);

                private It should_be_stored_in_the_default_underlying_dependencies = () =>
                    dependencies[typeof(IDbConnection)][""].ShouldEqual(the_connection);

                It should_return_the_instance_being_registered = () =>
                    result.ShouldEqual(the_connection);

                static IDbConnection the_connection;
                static IDbConnection result;
            }
            public class and_it_has_already_been_stored : when_storing_default_dependencies
            {
                Establish c = () =>
                {
                    the_new_connection = fake.an<IDbConnection>();
                    sut.on(the_connection);
                };

                Because b = () =>
                    result = sut.on(the_new_connection);

                It should_replace_the_existing_default_instance = () =>
                    dependencies[typeof(IDbConnection)][""].ShouldEqual(the_new_connection);

                It should_return_the_instance_being_registered = () =>
                    result.ShouldEqual(the_new_connection);

                static IDbConnection result;
                static IDbConnection the_new_connection;
            }

            public class and_the_dependency_is_not_being_provided : when_storing_default_dependencies
            {
                Establish c = () =>
                {
                    the_connection_created_by_the_fake_gateway = fake.an<IDbConnection>();
                    fake_gateway.setup(x => x.the<IDbConnection>()).Return(the_connection_created_by_the_fake_gateway);
                };

                Because b = () =>
                    result = sut.on<IDbConnection>();

                private It should_store_the_item_created_by_the_fake_gateway_as_the_default = () =>
                    dependencies[typeof(IDbConnection)][""].ShouldEqual(the_connection_created_by_the_fake_gateway);

                It should_return_the_item_created_by_the_fake_gateway = () =>
                    result = the_connection_created_by_the_fake_gateway;


                static IDbConnection the_connection_created_by_the_fake_gateway;
                static IDbConnection result;
            }
        }
    }
}