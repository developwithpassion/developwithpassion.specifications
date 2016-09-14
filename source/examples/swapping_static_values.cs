using System.Security;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.interactions;
using developwithpassion.specifications.assertions.type_specificity;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.examples
{
  public interface ISpecifySecurity
  {
    bool IsInRole(string name);    
  }

  public class TheThread
  {
    public static ISpecifySecurity CurrentPrincipal { get; set; }
  }
  public class swapping_static_values
  {
    [Subject(typeof(Calculator))]
    public class when_shutting_off_the_calculator_and_they_are_not_in_the_correct_security_role :
      use_engine<MoqFakeEngine>.observe<Calculator>
    {
      Establish c = () =>
      {
        fake_principal = fake.an<ISpecifySecurity>();

        fake_principal.setup(x => x.IsInRole("blah")).Return(false);
        //The change method is what allows you to swap the value of a static property or
        //field solely for the duration of a test. After the test completes it will be
        //reset back to its original value
        spec.change(() => TheThread.CurrentPrincipal).to(fake_principal);
      };

      Because b = () =>
        spec.catch_exception(() => sut.shut_off());

      It should_throw_a_security_exception = () =>
        spec.exception_thrown.should().be_an<SecurityException>();

      static ISpecifySecurity fake_principal;
    }

    public class Calculator
    {
      public void shut_off()
      {
        if (TheThread.CurrentPrincipal.IsInRole("blah")) return;
        throw new SecurityException();
      }
    }
  }
}
