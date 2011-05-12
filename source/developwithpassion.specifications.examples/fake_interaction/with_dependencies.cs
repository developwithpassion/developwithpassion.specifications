using System.Data;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.fake_interaction
{
  public class with_dependencies
  {
    public abstract class concern : Observes<Calculator>
    {
    }

    [Subject(typeof(Calculator))]
    public class when_adding_numbers : concern
    {
      Establish c = () => { connection = depends.on<IDbConnection>(); };

      Because b = () =>
        result = sut.add(2, 3);

      It should_open_the_connection = () =>
        //the received method is how you assert whether a call was made to a fake
        connection.received(x => x.Open());

      It should_return_the_sum = () =>
        result.ShouldEqual(5);

      static int result;
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
        connection.Open();
        return first + second;
      }
    }
  }
}