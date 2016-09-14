using System;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples
{
  public class exception_handling
  {
    [Subject(typeof(Calculator))]
    public class when_adding_2_numbers : use_engine<MoqFakeEngine>.observe
    {
      Because b = () =>
        spec.catch_exception(() => Calculator.add(2, 3));

      It should_throw_an_arugment_exception = () =>
        spec.exception_thrown.should().be_an<ArgumentException>();
    }

    public class Calculator
    {
      public static void add(int first, int second)
      {
        throw new ArgumentException();
      }
    }
  }
}
