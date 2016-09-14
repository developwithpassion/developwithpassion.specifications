using Machine.Fakes;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.fake_interaction
{
  public class with_dependencies
  {
    public interface IConnect
    {
      void Open();
    }

    public interface IAdapt
    {
    }

    public abstract class concern : use_engine<MoqFakeEngine>.observe<Calculator>
    {
    }

    [Subject(typeof(Calculator))]
    public class when_adding_numbers : concern
    {
      Establish c = () =>
      {
        connection = depends.on<IConnect>();
      };

      Because b = () =>
        result = sut.add(2, 3);

      It should_open_the_connection = () =>
        //the received method is how you assert whether a call was made to a fake
        connection.WasToldTo(x => x.Open());

      It should_return_the_sum = () =>
        result.ShouldEqual(5);

      static int result;
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
        connection.Open();
        return first + second;
      }
    }
  }
}