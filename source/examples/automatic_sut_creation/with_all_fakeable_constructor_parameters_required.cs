using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
  public interface IConnect
  {
    
  }

  public interface IAdapt
  {
    
  }

  public class with_all_fakeable_constructor_parameters_required
  {
    [Subject(typeof(Calculator))]
    public class when_adding_two_numbers : use_engine<MoqFakeEngine>.observe<Calculator>
    {
      Because b = () =>
        result = sut.add(3, 2);

      It should_return_the_sum = () => result.ShouldEqual(5);

      static int result;
    }

    public class Calculator
    {
      IConnect connection;
      IAdapt adapter;

      public Calculator(IConnect connection, IAdapt adapter)
      {
        this.connection = connection;
        this.adapter = adapter;
      }

      public int add(int first, int second)
      {
        return first + second;
      }
    }
  }
}
