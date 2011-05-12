using System.Data;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specification.specs
{
  [Subject(typeof(SomeItem))]
  public class nested_contexts_specs
  {
    public abstract class concern : Observes<ISomeItem, SomeItem>
    {
    }

    public class parent_concern : concern
    {
      public class when_leveraging_fields_that_are_initialized_from_a_containing_concerns_base_type
      {
        It
          fields_should_be_able_to_be_accessed_by_the_nested_class
            = () =>
              fake.an<IDbConnection>().ShouldNotBeNull();
      }
    }

    public interface ISomeItem
    {
    }

    public class SomeItem : ISomeItem
    {
    }
  }
}