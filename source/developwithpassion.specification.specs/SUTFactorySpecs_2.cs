using System.Data;
using Machine.Specifications;
using developwithpassion.specifications.faking;

namespace developwithpassion.specification.specs
{
    public partial class SUTFactorySpecs
    {
        [Subject(typeof(DefaultSUTFactory<>))]
        public class when_creating_a_type_that_has_all_of_its_dependencies_specified_as_fields :
            concern
        {
            static ItemWithAllDependenciesAsPublicFields result;
            static DefaultSUTFactory<ItemWithAllDependenciesAsPublicFields> sut;

            Establish c = () =>
            {
                sut = create_sut<ItemWithAllDependenciesAsPublicFields>();
            };

            public class and_all_of_the_arguments_have_been_explicitly_provided :
                when_creating_a_type_that_has_all_of_its_dependencies_specified_as_fields
            {

                Because b = () =>
                    result = sut.create();

                It should_have_correctly_set_all_of_the_dependencies_on_the_type = () =>
                {
                    result.Adapter.ShouldEqual(adapter);
                    result.Connection.ShouldEqual(connection);
                };

                static IDbConnection connection;
                static IDbDataAdapter adapter;
            }
        }

        public class ItemWithAllDependenciesAsPublicFields
        {
            public IDbConnection Connection;
            public IDbDataAdapter Adapter;
        }
    }
}