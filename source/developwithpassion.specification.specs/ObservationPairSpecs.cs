using developwithpassion.specifications;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class ObservationPairSpecs
    {
        public class concern : Observes<ObservationPair>
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new ObservationPair(() => setup_ran = true,
                                                                   () => teardown_ran = true));
            };

            Cleanup cu = () =>
            {
                teardown_ran = false;
                setup_ran = false;
            };

            protected static bool teardown_ran;
            protected static bool setup_ran;
        }

        [Subject(typeof(ObservationPair))]
        public class when_told_to_teardown : concern
        {
            Because b = () =>
                sut.teardown();

            It should_only_run_its_teardown_block = () =>
            {
                setup_ran.ShouldBeFalse();
                teardown_ran.ShouldBeTrue();
            };

            [Subject(typeof(ObservationPair))]
            public class when_told_to_setup : concern
            {
                Because b = () =>
                    sut.setup();

                It should_only_run_its_teardown_block = () =>
                {
                    setup_ran.ShouldBeTrue();
                    teardown_ran.ShouldBeFalse();
                };
            }
        }
    }
}