using developwithpassion.specifications.extensions;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
    public class StringExtensionsSpecs
    {
        [Subject(typeof(StringExtensions))]
        public class when_a_string_is_formatted_with_arguments
        {
            Because b = () =>
                result = "this is the {0};".format_using(new object[] {1});

            static string result;

            It should_return_the_string_formatted_with_the_arguments_specified = () =>
                result.ShouldEqual("this is the 1;");
        }
    }
}