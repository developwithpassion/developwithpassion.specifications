using System;
using Machine.Specifications;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specifications.examples
{
    public class exception_handling
    {
        [Subject(typeof(Calculator))]
        public class when_adding_2_numbers : Observes
        {
            Because b = () =>
                spec.catch_exception(() => Calculator.add(2, 3));

            It should_throw_an_arugment_exception = () =>
                spec.exception_thrown.ShouldBeAn<ArgumentException>();
        }

        public class Calculator
        {
            public static void add(int first, int second)
            {
                throw new ArgumentException();
            }
        }
    }
}