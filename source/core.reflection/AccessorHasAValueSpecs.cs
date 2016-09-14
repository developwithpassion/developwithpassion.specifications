using System;
using developwithpassion.specifications.assertions.interactions;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;

namespace developwithpassion.specifications.core.reflection
{
  [Subject(typeof(AccessorHasAValue))]
  public class AccessorHasAValueSpecs
  {
    public interface IConnect
    {
    }

    public class Connector : IConnect
    {
      
    }

    public interface IAdapt
    {
    }

    public class Adapter : IAdapt
    {
      
    }

    public abstract class concern : use_engine<MoqFakeEngine>.observe
    {
      Establish c = () =>
      {
        target = new TheItem();
        sut = new AccessorHasAValue(target);
        accessor = fake.an<MemberAccessor>();
      };

      protected static AccessorHasAValue sut;
      protected static TheItem target;
      protected static MemberAccessor accessor;
    }

    public class when_determining_if_an_accessor_has_a_value : concern
    {
      public class and_it_represents_a_struct : when_determining_if_an_accessor_has_a_value
      {
        Establish c = () =>
        {
          accessor.setup(x => x.accessor_type).Return(typeof(int));
        };

        Because b = () =>
          result = sut.matches(accessor);

        It should_always_have_a_value = () =>
          result.ShouldBeTrue();
      }

      public class and_it_represents_a_class_with_a_value : when_determining_if_an_accessor_has_a_value
      {
        Establish c = () =>
        {
          accessor.setup(x => x.accessor_type).Return(typeof(Connector));
          accessor.setup(x => x.get_value(target)).Return(new Connector());
        };

        Because b = () =>
          result = sut.matches(accessor);

        It should_have_a_value = () =>
          result.ShouldBeTrue();
      }

      public class and_it_represents_a_class_without_a_value : when_determining_if_an_accessor_has_a_value
      {
        Establish c = () =>
        {
          accessor = new FakeAccesor {accessor_type = typeof(Connector)};
        };

        Because b = () =>
          result = sut.matches(accessor);

        It should_not_match = () =>
          result.ShouldBeFalse();

        class FakeAccesor : MemberAccessor
        {
          public object provided_target;

          public Type declaring_type
          {
            get { throw new NotImplementedException(); }
          }

          public string name
          {
            get { throw new NotImplementedException(); }
          }

          public Type accessor_type { get; set; }

          public void change_value_to(object target, object new_value)
          {
            throw new NotImplementedException();
          }

          public object get_value(object target)
          {
            provided_target = target;
            return null;
          }
        }
      }

      static bool result;
    }

    public class TheItem
    {
      public IConnect connection { get; set; }
      public int number { get; set; }
    }
  }
}