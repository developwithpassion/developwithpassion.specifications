using System;
using System.Data;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
    public class with_a_mix_of_fakeable_and_two_non_fakeable_of_the_same_type_contructor_parameters
    {
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_specific_date_and_times : Observes<Calculator>
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_end_date = dateTime.Add(TimeSpan.FromDays(8));
                expected_date_on_property = dateTime.Add(TimeSpan.FromDays(16));
                depends.on(expected_current_date, "current_date");
                depends.on(expected_end_date, "end_date"); 
                depends.on(expected_date_on_property, "date_on_property"); 
            };
            
            Because b = () =>
                result = sut.add(2, 3);

            It should_have_expected_current_date = () =>
                sut.current_date.ShouldEqual(expected_current_date);

            It should_have_expected_end_date = () =>
                sut.end_date.ShouldEqual(expected_end_date);
            
            It should_have_expected_date_on_property = () =>
                sut.date_on_property.ShouldEqual(expected_date_on_property);

            It should_return_the_sum = () =>
                result.ShouldEqual(5);

            static int result;
            static DateTime expected_end_date;
            static DateTime expected_current_date;
            static DateTime expected_date_on_property;
        }
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_specific_date_and_time_and_default_date_and_time : Observes<Calculator>
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_date_on_property = dateTime.Add(TimeSpan.FromDays(16));
                depends.on(expected_current_date, "current_date");
                depends.on(expected_date_on_property, "date_on_property"); 
            };
            
            Because b = () =>
                result = sut.add(2, 3);

            It should_have_expected_current_date = () =>
                sut.current_date.ShouldEqual(expected_current_date);

            It should_have_default_date_time_for_end_time = () =>
                sut.end_date.ShouldEqual(DateTime.MinValue);
            
            It should_have_expected_date_on_property = () =>
                sut.date_on_property.ShouldEqual(expected_date_on_property);

            It should_return_the_sum = () =>
                result.ShouldEqual(5);

            static int result;
            static DateTime expected_current_date;
            static DateTime expected_date_on_property;
        }
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_specific_date_and_time_and_no_default_date_and_time : Observes<Calculator>
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_date_on_property = dateTime.Add(TimeSpan.FromDays(16));
                depends.on(expected_current_date, "current_date");
            };
            
            Because b = () =>
                result = sut.add(2, 3);

            It should_have_expected_current_date = () =>
                sut.current_date.ShouldEqual(expected_current_date);

            It should_have_default_date_time_for_end_time = () =>
                sut.end_date.ShouldEqual(DateTime.MinValue);
            
            It should_have_expected_date_on_property = () =>
                sut.date_on_property.ShouldEqual(DateTime.MinValue);

            It should_return_the_sum = () =>
                result.ShouldEqual(5);

            static int result;
            static DateTime expected_current_date;
            static DateTime expected_date_on_property;
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

            public DateTime date_on_property { get; set; }

            public int add(int first, int second)
            {
                return first + second;
            }
        }
    }
}