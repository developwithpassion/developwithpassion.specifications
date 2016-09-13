using System.Data;
using Machine.Fakes;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.fake_interaction
{
  public class with_dependencies
  {
    public abstract class concern : use_engine<RhinoFakeEngine>.observe<Calculator>
    {
    }

    [Subject(typeof(Calculator))]
    public class when_adding_numbers : concern
    {
      Establish c = () =>
      {
        connection = depends.on<IDbConnection>();
      };

      Because b = () =>
        result = sut.add(2, 3);

      It should_open_the_connection = () =>
        //the received method is how you assert whether a call was made to a fake
        connection.WasToldTo(x => x.Open());

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