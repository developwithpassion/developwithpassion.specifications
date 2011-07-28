using System;
using System.Data;
using Machine.Specifications;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
    public class with_a_mix_of_dependencies_in_the_ctor_properties_and_fields
    {
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers : Observes<Calculator>
        {
            Establish c = () =>
                depends.on(the_date);

            Because b = () =>
                result = sut.add(2, 3);

            It should_return_the_sum = () =>
                result.ShouldEqual(5);

            static int result;
            static DateTime the_date = new DateTime(2011, 1, 1);
        }

        public delegate void SomeDelegate();

        public class Calculator
        {
            IDataAdapter adapter;
            public IDbConnection connection;
            public DateTime current_date { get; set; }
            public SomeDelegate some_delegate;

            public Calculator(IDataAdapter adapter)
            {
                this.adapter = adapter;
            }

            public int add(int first, int second)
            {
                return first + second;
            }
        }
    }
}