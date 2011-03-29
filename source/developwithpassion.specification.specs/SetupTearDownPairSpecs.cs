using Machine.Specifications;

namespace developwithpassion.specifications.rhinomocks
{
    public class SetupTearDownPairSpecs
    {
        public class concern : Observes<SetupTearDownPair>
        {
        }

        [Subject(typeof(SetupTearDownPair))]
        public class when_told_to_finish : concern
        {
            Establish c = () =>
            {
                sut_factory.create_using(() => new SetupTearDownPair(() => setup_ran = true,
                                                                     () => teardown_ran = true));
            };

            Because b = () =>
                sut.finish();

            It should_only_run_its_teardown_block = () =>
            {
                setup_ran.ShouldBeFalse();
                teardown_ran.ShouldBeTrue();
            };

            protected static bool teardown_ran;
            protected static bool setup_ran;

            [Subject(typeof(SetupTearDownPair))]
            public class when_told_to_start : concern
            {
                Establish c = () =>
                {
                    sut_factory.create_using(() => new SetupTearDownPair(() => setup_ran = true,
                                                                         () => teardown_ran = true));
                };

                Because b = () =>
                    sut.start();

                It should_only_run_its_teardown_block = () =>
                {
                    setup_ran.ShouldBeTrue();
                    teardown_ran.ShouldBeFalse();
                };

                protected static bool teardown_ran;
                protected static bool setup_ran;
            }
        }
    }
}