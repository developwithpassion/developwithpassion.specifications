using System.Data;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications.examples
{
    [Subject(typeof(SimpleCalculator))]
    public class nested_contexts
    {
        public abstract class concern : Observes<SimpleCalculator>
        {
        }

        public class when_adding_two_numbers : concern
        {
            Establish c = () => { connection = depends.on<IDbConnection>(); };

            Because b = () =>
                result = sut.add(first_number, second_number);

            public class and_both_numbers_are_positive : concern
            {
                Establish c = () =>
                {
                    first_number = 2;
                    second_number = 3;
                };

                It should_not_open_the_db_connection = () =>
                    connection.never_received(x => x.Open());
            }

            public class and_one_of_the_numbers_are_negative : concern
            {
                Establish c = () =>
                {
                    first_number = 2;
                    second_number = -1;
                };

                It should_open_a_connection_to_the_database = () =>
                    connection.received(x => x.Open());
            }

            static IDbConnection connection;
            static int result;
            static int second_number;
            static int first_number;
        }
    }

    public class SimpleCalculator
    {
        IDbConnection connection;

        public SimpleCalculator(IDbConnection connection)
        {
            this.connection = connection;
        }

        public int add(int number1, int number2)
        {
            if (number1 < 0 || number2 < 0) connection.Open();
            return number1 + number2;
        }
    }
}