using System;
using System.Data;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
    public class with_a_mix_of_fakeable_and_two_non_fakeable_of_the_same_type_contructor_parameters
    {
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers : Observes<Calculator>
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_end_date = dateTime.Add(TimeSpan.FromDays(8));
                depends.on(expected_current_date, "current_date");
                depends.on(expected_end_date, "end_date"); 
            };
            
            Because b = () =>
                result = sut.add(2, 3);

            It should_have_expected_current_date = () =>
                sut.current_date.ShouldEqual(expected_current_date);

            It should_have_expected_end_date = () =>
                sut.end_date.ShouldEqual(expected_end_date);

            It should_return_the_sum = () =>
                result.ShouldEqual(5);

            static int result;
            static DateTime expected_end_date;
            static DateTime expected_current_date;
        }

        public delegate void SomeDelegate();

        public class Calculator
        {
            IDbConnection connection;
            IDataAdapter adapter;
            readonly SomeDelegate some_delegate;

            public Calculator(IDbConnection connection, IDataAdapter adapter, DateTime current_date, DateTime end_date,
                              SomeDelegate some_delegate)
            {
                this.connection = connection;
                this.adapter = adapter;
                this.current_date = current_date;
                this.end_date = end_date;
                this.some_delegate = some_delegate;
            }

            public DateTime current_date { get; private set; }

            public DateTime end_date { get; private set; }

            public int add(int first, int second)
            {
                return first + second;
            }
        }
    }
}