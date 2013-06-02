using System;
using System.Data;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples.automatic_sut_creation
{
    public class with_a_mix_of_fakeable_and_two_non_fakeable_of_the_same_type_contructor_parameters
    {
        public abstract class base_calculator_specification : Observes<Calculator>
        {
            protected static Exception catch_exception(Action action)
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    return e;
                }

                return null;
            }
        }

        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_two_date_times_by_name : base_calculator_specification
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
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_two_date_times_without_name : base_calculator_specification
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_end_date = dateTime.Add(TimeSpan.FromDays(8));
                depends.on(expected_current_date);
                caught_exception = catch_exception(() => depends.on(expected_end_date));
            };

            It should_have_expected_exception = () =>
                caught_exception.Message.ShouldEqual("To specify multiple System.DateTime, use depends.on(value, name)");

            static DateTime expected_end_date;
            static DateTime expected_current_date;
            static Exception caught_exception;
        }
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_two_date_times_one_with_name_and_other_without_name : base_calculator_specification
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                expected_end_date = dateTime.Add(TimeSpan.FromDays(8));
                depends.on(expected_current_date, "current_date");
                caught_exception_when_second_depends_is_called = catch_exception(() => depends.on(expected_end_date));
                sut_factory.during_create(sut_creation =>
                                              {
                                                  caught_exception_when_creating_sut = catch_exception(() => sut_creation());
                                                  return null;
                                              });
            };

            It should_throw_expected_exception_when_second_depends_is_called = () =>
                caught_exception_when_second_depends_is_called.Message.ShouldEqual("To specify multiple System.DateTime, use depends.on(value, name)");

            It should_throw_expected_exception_during_sut_creation = () =>
                caught_exception_when_creating_sut.Message.ShouldEqual("You must specify dependency of type System.DateTime and name end_date, use depends.on(value, name)");

            static DateTime expected_end_date;
            static DateTime expected_current_date;
            static Exception caught_exception_when_second_depends_is_called;
            static Exception caught_exception_when_creating_sut;
        }
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_two_date_times_but_only_specifying_one_by_name : base_calculator_specification
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                depends.on(expected_current_date, "current_date");
                sut_factory.during_create(sut_creation =>
                                              {
                                                  caught_exception_when_creating_sut = catch_exception(() => sut_creation());
                                                  return null;
                                              });
            };

            It should_throw_expected_exception_during_sut_creation = () =>
                caught_exception_when_creating_sut.Message.ShouldEqual("You must specify dependency of type System.DateTime and name end_date, use depends.on(value, name)");

            static DateTime expected_current_date;
            static Exception caught_exception_when_creating_sut;
        }
        
        [Subject(typeof(Calculator))]
        public class when_adding_two_numbers_with_two_date_times_specified_by_name_but_one_name_is_wrong : base_calculator_specification
        {
            private Establish c = () =>
            {
                var dateTime = DateTime.Now;
                expected_current_date = dateTime;
                depends.on(expected_current_date, "current_date");
                depends.on(expected_current_date.AddDays(8), "endDate");
                sut_factory.during_create(sut_creation =>
                                              {
                                                  caught_exception_when_creating_sut = catch_exception(() => sut_creation());
                                                  return null;
                                              });
            };

            It should_throw_expected_exception_during_sut_creation = () =>
                caught_exception_when_creating_sut.Message.ShouldEqual("You must specify dependency of type System.DateTime and name end_date, use depends.on(value, name)");

            static DateTime expected_current_date;
            static Exception caught_exception_when_creating_sut;
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