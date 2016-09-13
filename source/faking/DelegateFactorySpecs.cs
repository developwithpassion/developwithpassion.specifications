using System;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.faking
{
  public class DelegateFactorySpecs
  {
    public class concern : use_engine<RhinoFakeEngine>.observe<ICreateFakeDelegates,
      FakeDelegateFactory>
    {
    }

    [Subject(typeof(FakeDelegateFactory))]
    public class when_creating_a_delegate_for_a_void_delegate_type : concern
    {
      Because b = () =>
        result = sut.generate_delegate_for(typeof(SomeDelegate));

      It should_create_a_delegate_of_the_correct_type = () =>
        result.should().be_an<SomeDelegate>();

      It should_create_a_delegate_that_does_not_throw_an_exception_when_invoked = () =>
        result.downcast_to<SomeDelegate>().Invoke();

      static object result;

      public delegate void SomeDelegate();
    }

    [Subject(typeof(FakeDelegateFactory))]
    public class when_creating_an_delegate_for_a_close_generic_type_with_a_return_value : concern
    {
      Because b = () =>
        result = sut.generate_delegate_for(typeof(Func<int, bool>));

      It should_create_a_delegate_of_the_correct_type = () =>
        result.should().be_an<Func<int, bool>>();

      It should_create_a_delegate_that_return_the_default_return_value_for_the_return_type = () =>
        result.downcast_to<Func<int, bool>>().Invoke(42).ShouldBeFalse();

      static object result;
    }
  }
}