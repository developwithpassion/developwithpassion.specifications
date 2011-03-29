using developwithpassion.specifications;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class FieldSwitcherSpecs
    {
        public abstract class concern : Observes<FieldSwitcher, DefaultFieldSwitcher>
        {
            Establish c = () =>
            {
                original_value = "sdfsdfs";
                target = depends.on<MemberTarget>();
            };

            protected static MemberTarget target;
            protected static string original_value;
        }

        [Subject(typeof(FieldSwitcher))]
        public class when_constructed : concern
        {
            It should_use_the_target_to_get_the_original_value = () =>
                target.received(x => x.get_value());

            static string value_to_change_to;
        }

        [Subject(typeof(FieldSwitcher))]
        public class when_provided_the_value_to_change_to : concern
        {
            Establish c = () =>
            {
                value_to_change_to = "sdfsdf";
                target.setup(x => x.get_value()).Return(original_value);
            };

            Because b = () =>
                result = sut.to(value_to_change_to);

            It should_provide_the_pipeline_pair_that_can_do_the_switching = () =>
            {
                result.start();
                target.received(x => x.change_value_to(value_to_change_to));
                result.finish();
                target.received(x => x.change_value_to(original_value));
            };

            protected static string original_value;
            protected static SetupTearDownPair result;
            protected static string value_to_change_to;
        }
    }
}