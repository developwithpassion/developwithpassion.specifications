using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples
{
  public class explicitly_creating_the_system_under_test
  {
    [Subject(typeof(Calculator))]
    public class when_adding_two_numbers : use_engine<RhinoFakeEngine>.observe<Calculator>
    {
      Establish c = () =>
      {
        //the sut_factory has a method that allows you to specify explicit
        //sut creation
        sut_factory.create_using(() => new Calculator(4, 5));
      };

      Because b = () =>
        result = sut.add(1, 3);

      It should_return_the_sum = () =>
        result.ShouldEqual(4);

      static int result;
    }
  }

  public class Calculator
  {
    public Calculator(int some_random_value, int some_other_random_value)
    {
    }

    public int add(int first, int second)
    {
      return first + second;
    }
  }
}