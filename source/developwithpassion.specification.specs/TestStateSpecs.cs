using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.faking;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class TestStateSpecs
    {
        public class SomeItem
        {
            public bool was_leveraged;
        }

        public class concern : Observes
        {
            Establish c = () =>
            {
                setup_behaviours = new List<ObservationPair>();
                factory = fake.an<ICreateAndManageDependenciesFor<SomeItem>>();
                sut_context_behaviours = new List<SUTContextSetup<SomeItem>>();
                sut = new DefaultTestStateFor<SomeItem>(factory);
                concrete_sut.setup_tear_down_pairs = setup_behaviours;
                concrete_sut.sut_context_behaviours = sut_context_behaviours;
            };

            protected static DefaultTestStateFor<SomeItem>  concrete_sut{get{ return sut.downcast_to<DefaultTestStateFor<SomeItem>>();}}
            protected static ICreateAndManageDependenciesFor<SomeItem> factory;
            protected static IList<ObservationPair> setup_behaviours;
            protected static TestStateFor<SomeItem> sut;
            protected static IList<SUTContextSetup<SomeItem>> sut_context_behaviours;
        }

        [Subject(typeof(DefaultTestStateFor<SomeItem>))]
        public class when_a_setup_behaviour_is_registered : concern
        {
            Establish c = () =>
            {
                the_behaviour = new ObservationPair(() => { }, () => { });
                some_item = new SomeItem();
            };

            Because b = () =>
                sut.add_setup_teardown_pair(the_behaviour);

            It should_store_it_in_the_list_of_behaviours_to_run_as_part_of_the_setup_process = () =>
                setup_behaviours.ShouldContain(the_behaviour);

            protected static SomeItem some_item;
            protected static ObservationPair the_behaviour;
        }

        [Subject(typeof(DefaultTestStateFor<SomeItem>))]
        public class when_a_sut_behaviour_is_registered : concern
        {
            Establish c = () => { behaviour = x => { }; };

            Because b = () =>
                sut.run(behaviour);

            It should_store_it_in_the_list_of_actions_to_run_against_the_sut = () =>
                sut_context_behaviours.ShouldContain(behaviour);

            protected static SUTContextSetup<SomeItem> behaviour;
        }

        [Subject(typeof(DefaultTestStateFor<>))]
        public class when_running_its_setup : concern
        {
            Establish c = () =>
            {
                item = new SomeItem();
                factory.setup(x => x.create()).Return(item);
                Enumerable.Range(1, 10).each(x => sut_context_behaviours.Add(
                    y => number_of_sut_setup_actions_ran++));
                Enumerable.Range(1, 10).each(x => setup_behaviours.Add(
                    new ObservationPair(() => { number_of_setup_actions_ran++; }, () => { })));
            };

            Because b = () =>
                result = sut.run_setup();

            It should_create_the_sut_using_the_sut_factory = () =>
                result.ShouldEqual(item);

            It should_run_all_of_setup_pipeline_actions = () =>
                number_of_setup_actions_ran.ShouldEqual(10);

            It should_run_each_of_the_sut_blocks_against_the_sut = () =>
                number_of_sut_setup_actions_ran.ShouldEqual(10);

            protected static SomeItem item;
            protected static int number_of_setup_actions_ran;
            protected static int number_of_sut_setup_actions_ran;
            protected static SomeItem result;
        }

        [Subject(typeof(DefaultTestStateFor<>))]
        public class when_running_its_teardown_pipeline : concern
        {
            Establish c = () =>
            {
                Enumerable.Range(1, 10).each(x =>
                {
                    setup_behaviours.Add(new ObservationPair(() => { },
                                                               () => { teardown_behaviours_ran++; }));
                });
            };

            Because b = () =>
                sut.run_tear_down();

            It should_run_each_of_the_finish_blocks_in_all_of_its_pipeline_behaviours = () =>
                teardown_behaviours_ran.ShouldEqual(10);

            protected static int teardown_behaviours_ran;
        }
    }
}