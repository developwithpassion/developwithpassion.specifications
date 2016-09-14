using System;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
  public class with_a_mix_of_dependencies_in_the_ctor_properties_and_fields
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
        depends.on(the_date);

      Because b = () =>
        result = sut.add(2, 3);

      It should_return_the_sum = () =>
        result.ShouldEqual(5);

      It should_set_the_date_correctly = () =>
        sut.current_date.ShouldEqual(the_date);

      It should_have_initialized_the_connection = () =>
        sut.connection.ShouldNotBeNull();

      It should_have_initialized_the_delegate = () =>
        sut.some_delegate.ShouldNotBeNull();

      static int result;
      static DateTime the_date = new DateTime(2011, 1, 1);
    }

    public delegate void SomeDelegate();

    public class Calculator
    {
      IAdapt adapter;
      public IConnect connection;
      public DateTime current_date { get; set; }
      public SomeDelegate some_delegate;

      public Calculator(IAdapt adapter)
      {
        this.adapter = adapter;
      }

      public int add(int first, int second)
      {
        return first + second;
      }
    }
  }
}