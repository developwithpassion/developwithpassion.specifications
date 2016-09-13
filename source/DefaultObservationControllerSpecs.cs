using System;
using developwithpassion.specifications.assertions.core;
using developwithpassion.specifications.assertions.type_specificity;
using developwithpassion.specifications.core.factories;
using developwithpassion.specifications.extensions;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;

namespace developwithpassion.specifications
{
  [Subject(typeof(DefaultObservationController<,>))]
  public class DefaultObservationControllerSpecs
  {
    public class integration
    {
      public abstract class concern : use_engine<RhinoFakeEngine>.observe
      {
        Establish c = () =>
        {
          sut = MainControllerFactory.new_instance().create_main_controller<AnItem, RhinoFakeEngine>()
            .downcast_to<DefaultObservationController<AnItem, RhinoFakeEngine>>();
        };

        protected static DefaultObservationController<AnItem, RhinoFakeEngine> sut;
      }

      public class when_catching_an_exception_around_a_string_based_property_invocation : concern
      {
        Establish c = () =>
        {
          the_item = new AnItem();
          sut.catch_exception(() => the_item.the_string);
        };

        Because b = () =>
          result = sut.exception_thrown;

        It should_catch_the_exception_correctly = () =>
          result.should().be_an<NotImplementedException>();

        static AnItem the_item;
        static Exception result;
      }

      public class AnItem
      {
        public string the_string
        {
          get { throw new NotImplementedException(); }
        }
      }
    }
  }
}