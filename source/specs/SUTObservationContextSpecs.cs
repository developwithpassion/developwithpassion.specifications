using System;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.observations;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.specs
{
  public class SUTObservationContextSpecs
  {
    public interface IConnect
    {
    }
    public interface ICommand
    {
    }
    public class concern : use_engine<MoqFakeEngine>.observe
    {
    }

    public class SomeClass
    {
    }

    [Behaviors]
    public class OtherBehaviours
    {
      protected static SomeClass sut;

      It should_update_the_item = () =>
        sut.ShouldNotBeNull();
    }

    public class SomeClassWithDependencies
    {
      public SomeClassWithDependencies(IConnect connection, ICommand command)
      {
        this.connection = connection;
        this.command = command;
      }

      public ICommand command { get; set; }
      public IConnect connection { get; set; }
    }

    [Subject(typeof(InstanceObservations<,,>))]
    public class when_a_test_has_run_its_setup : concern
    {
      protected static int number;

      Establish c = () =>
        pipeline.add_setup_teardown_pair(() =>
        {
          number++;
        }, () =>
        {
        });

      It should_be_able_to_modify_the_test_pipeline = () =>
        number.ShouldEqual(1);
    }

    [Subject(typeof(InstanceObservations<,,>))]
    public class when_a_test_is_run_and_the_sut_requires_dependencies :
      use_engine<MoqFakeEngine>.observe<SomeClassWithDependencies>
    {
      It should_have_automatically_mocked_out_the_dependencies_required_by_the_sut = () =>
      {
        sut.connection.ShouldNotBeNull();
        sut.command.ShouldNotBeNull();
      };
    }

    [Subject(typeof(InstanceObservations<,,>))]
    public class when_a_test_is_run_that_requires_a_sut : use_engine<MoqFakeEngine>.observe<SomeClass>
    {
      It should_have_automatically_created_the_sut = () =>
        sut.ShouldBeOfExactType<SomeClass>();
    }

    [Subject("something")]
    public class when_run_with_other_behaviours : use_engine<MoqFakeEngine>.observe<SomeClass>
    {
      Behaves_like<OtherBehaviours> behaviours;

      It should_run = () =>
        true.ShouldBeTrue();
    }

    [Subject(typeof(InstanceObservations<,,>))]
    public class when_an_error_occurs_during_the__test : use_engine<MoqFakeEngine>.observe<SomeClassWithDependencies>
    {
      Because b = () =>
        spec.catch_exception(() => throw_something());

      Establish context = delegate
      {
        connection = fake.an<IConnect>();
        command = fake.an<ICommand>();
        sut_factory.create_using(() => new SomeClassWithDependencies(connection, command));
      };

      static void throw_something()
      {
        throw new Exception();
      }

      It should_be_able_to_access_the_exception_thrown_by_the_sut = () =>
        spec.exception_thrown.should().be_an<Exception>();

      static ICommand command;
      static IConnect connection;
    }
  }
}
