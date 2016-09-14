using System;
using System.Collections.Generic;
using developwithpassion.specifications.assertions.interactions;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.faking
{
  [Subject(typeof(DependenciesRegistry))]
  public class DependenciesRegistrySpecs
  {
    public interface IConnect
    {
    }

    public interface ICommand
    {
    }

    public abstract class concern : use_engine<MoqFakeEngine>.observe
    {
      Establish c = () =>
      {
        dependency_resolver = fake.an<IResolveADependencyForTheSUT>();
        fake_gateway = fake.an<IManageFakes>();
        dependencies = new Dictionary<Type, object>();
        sut = new DependenciesRegistry(dependency_resolver, fake_gateway);
        sut.downcast_to<DependenciesRegistry>().explicit_dependencies = dependencies;
      };

      protected static IDictionary<Type, object> dependencies;
      protected static IManageTheDependenciesForASUT sut;
      protected static IResolveADependencyForTheSUT dependency_resolver;
      protected static IManageFakes fake_gateway;
    }

    public class when_asked_if_it_has_an_explicit_dependency : concern
    {
      Establish c = () =>
      {
        dependencies.Add(typeof(IConnect), fake.an<IConnect>());
      };

      It should_make_the_decision_based_on_whether_an_explicit_dependency_was_registered = () =>
      {
        sut.has_been_provided_an(typeof(IConnect)).ShouldBeTrue();
        sut.has_been_provided_an(typeof(ICommand)).ShouldBeFalse();
      };
    }

    public class when_getting_a_dependency : concern
    {
      public class and_the_dependency_has_been_explicitly_provided : when_getting_a_dependency
      {
        Establish c = () =>
        {
          the_connection = fake.an<IConnect>();
          dependencies.Add(typeof(IConnect), the_connection);
        };

        Because b = () =>
          result = sut.get_dependency_of(typeof(IConnect));

        It should_return_the_item_that_was_registered = () =>
          result.ShouldEqual(the_connection);

        static object result;
        static IConnect the_connection;
      }

      public class and_the_dependency_was_not_explicitly_provided : when_getting_a_dependency
      {
        Establish c = () =>
        {
          the_connection = fake.an<IConnect>();
          dependency_resolver.setup(x => x.resolve(typeof(IConnect))).Return(the_connection);
        };

        Because b = () =>
          result = sut.get_dependency_of(typeof(IConnect));

        It should_return_the_item_created_by_the_dependency_resolver = () =>
          result.ShouldEqual(the_connection);

        static object result;
        static IConnect the_connection;
      }
    }

    public class when_storing_a_dependency : concern
    {
      public class and_it_has_not_already_been_stored : when_storing_a_dependency
      {
        Establish c = () =>
        {
          the_connection = fake.an<IConnect>();
        };

        Because b = () =>
          result = sut.on(the_connection);

        It should_be_stored_in_the_underlying_dependencies = () =>
          dependencies[typeof(IConnect)].ShouldEqual(the_connection);

        It should_return_the_instance_being_registered = () =>
          result.ShouldEqual(the_connection);

        static IConnect the_connection;
        static IConnect result;
      }

      public class and_it_has_already_been_stored : when_storing_a_dependency
      {
        Establish c = () =>
        {
          the_connection = fake.an<IConnect>();
          the_new_connection = fake.an<IConnect>();
          sut.on(the_connection);
        };

        Because b = () =>
          result = sut.on(the_new_connection);

        It should_replace_the_existing_instance = () =>
          dependencies[typeof(IConnect)].ShouldEqual(the_new_connection);

        It should_return_the_instance_being_registered = () =>
          result.ShouldEqual(the_new_connection);

        static IConnect the_connection;
        static IConnect result;
        static IConnect the_new_connection;
      }

      public class and_the_dependency_is_not_being_provided : when_storing_a_dependency
      {
        Establish c = () =>
        {
          the_connection_created_by_the_fake_gateway = fake.an<IConnect>();
          fake_gateway.setup(x => x.the<IConnect>()).Return(the_connection_created_by_the_fake_gateway);
        };

        Because b = () =>
          result = sut.on<IConnect>();

        It should_store_the_item_created_by_the_fake_gateway = () =>
          dependencies[typeof(IConnect)].ShouldEqual(the_connection_created_by_the_fake_gateway);

        It should_return_the_item_created_by_the_fake_gateway = () =>
          result = the_connection_created_by_the_fake_gateway;

        static IConnect the_connection_created_by_the_fake_gateway;
        static IConnect result;
      }
    }
  }
}