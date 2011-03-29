using System.Security.Principal;
using System.Threading;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class ChangeSpecs
    {
        public abstract class concern : Observes
        {
        }

        public class SomeStatic
        {
            public static string some_value = "lah";
        }

        public class when_a_change_is_made_in_an_establish_block : concern
        {
            Establish c = delegate
            {
                new_value = "other_value";
                spec.change(() => SomeStatic.some_value).to(new_value);
            };

            It should_have_caused_the_change_to_the_field = () =>
                SomeStatic.some_value.ShouldEqual(new_value);

            static string new_value;
        }

        public class when_swapping_the_thread_current_principal : concern
        {
            Establish c = delegate
            {
                principal = fake.an<IPrincipal>();
                spec.change(() => Thread.CurrentPrincipal).to(principal);
            };

            It should_have_the_fake_principal_being_used_as_the_principal = () =>
                Thread.CurrentPrincipal.ShouldEqual(principal);

            static IPrincipal principal;
        }
    }
}