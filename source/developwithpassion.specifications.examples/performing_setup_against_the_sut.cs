using System;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples
{
    public class performing_setup_against_the_sut
    {

        [Subject(typeof(Accumulator))]
        public class when_it_has_visited_its_second_item : Observes<Accumulator>
        {
            Establish c = () =>
                //Use the sut_setup.run method to trigger a method against the sut
                //as part of setup. The sut field is not assigned until all Establish
                //blocks have run. This is why a delegate that accepts the type of "SUT"
                //is used to specify the behaviour to trigger, prior to the because  block
                sut_setup.run(x => x.visit(2));

            Because b = () =>
                sut.visit(3);


            It should_accumulate_the_numbers = () =>
                sut.result.ShouldEqual(5);

        }

        public class Accumulator
        {
            public int result { get; private set; }

            public void visit(int number)
            {
                result+=number;
            }
        }
    }
}