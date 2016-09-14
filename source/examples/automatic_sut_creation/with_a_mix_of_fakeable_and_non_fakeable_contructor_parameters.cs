using System;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
  public class with_a_mix_of_fakeable_and_non_fakeable_contructor_parameters
  {
    public interface IConnect
    {
    }

    public interface IAdapt
    {
    }

    [Subject(typeof(Calculator))]
      public class when_adding_two_numbers : use_engine<MoqFakeEngine>.observe<Calculator>
      {
        Establish c = () =>
          depends.on(DateTime.Now);

        Because b = () =>
          result = sut.add(2, 3);

        It should_return_the_sum = () =>
          result.ShouldEqual(5);

        static int result;
      }

      public delegate void SomeDelegate();

      public class Calculator
      {
        IConnect connection;
        IAdapt adapter;
        DateTime current_date;
        readonly SomeDelegate some_delegate;

        public Calculator(IConnect connection, IAdapt adapter, DateTime current_date,
          SomeDelegate some_delegate)
        {
          this.connection = connection;
          this.adapter = adapter;
          this.current_date = current_date;
          this.some_delegate = some_delegate;
        }

        public int add(int first, int second)
        {
          return first + second;
        }
      }
    }
  }