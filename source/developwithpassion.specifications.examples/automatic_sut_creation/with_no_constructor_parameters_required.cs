

using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
    public class with_no_constructor_parameters_required
    {
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers : Observes<Calculator>
        {
            Because b = () =>
                result = sut.add(3, 2);

            It should_return_the_sum = () =>
                5.ShouldEqual(result);

            static int result;
        }

        public class Calculator
        {
            public int add(int first, int second)
            {
                return first + second;
            }
        }
    }
}