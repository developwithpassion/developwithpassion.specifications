using developwithpassion.specifications.assertions.interactions;
using Machine.Fakes;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.fake_interaction
{
  public class with_fakes
  {
    public interface IConnect
    {
      void Open();
      ICommand CreateCommand();
    }

    public interface ICommand
    {
      void ExecuteNonQuery();
    }

    public interface IAdapt
    {
    }

    public abstract class concern : use_engine<MoqFakeEngine>.observe<Calculator>
    {
    }

    [Subject(typeof(Calculator))]
    public class when_adding_two_numbers : concern
    {
      Establish c = () =>
      {
        connection = depends.on<IConnect>();
        command = fake.an<ICommand>();

        //the setup method is how you instruct the fakes to return fake
        //data to a caller
        connection.setup(x => x.CreateCommand()).Return(command);
      };

      Because b = () =>
        result = sut.add(2, 3);

      It should_run_a_command = () =>
        command.WasToldTo(x => x.ExecuteNonQuery());

      It should_return_the_sum = () =>
        result.ShouldEqual(5);

      static int result;
      static ICommand command;
      static IConnect connection;
    }

    public class Calculator
    {
      IConnect connection;

      public Calculator(IConnect connection)
      {
        this.connection = connection;
      }

      public int add(int first, int second)
      {
        connection.CreateCommand().ExecuteNonQuery();
        return first + second;
      }
    }
  }
}