using System;
using Machine.Fakes.Adapters.Rhinomocks;
using Machine.Specifications;
using developwithpassion.specifications;
using developwithpassion.specifications.core.factories;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace developwithpassion.specification.specs
{
    [Subject(typeof(DefaultObservationController<,>))]
    public class DefaultObservationControllerSpecs
    {
        public class integration
        {
            public abstract class concern : Observes
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
                    result.ShouldBeAn<NotImplementedException>();

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