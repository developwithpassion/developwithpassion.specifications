using System.Data;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.fake_interaction
{
  public class with_fakes
  {
    public abstract class concern : Observes<Calculator>
    {
    }

    [Subject(typeof(Calculator))]
    public class when_adding_two_numbers : concern
    {
      Establish c = () =>
      {
        connection = depends.on<IDbConnection>();
        command = fake.an<IDbCommand>();

        //the setup method is how you instruct the fakes to return fake
        //data to a caller
        connection.setup(x => x.CreateCommand()).Return(command);
      };

      Because b = () =>
        result = sut.add(2, 3);

      It should_run_a_command = () =>
        command.received(x => x.ExecuteNonQuery());

      It should_return_the_sum = () =>
        result.ShouldEqual(5);

      static int result;
      static IDbCommand command;
      static IDbConnection connection;
    }

    public class Calculator
    {
      IDbConnection connection;

      public Calculator(IDbConnection connection)
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