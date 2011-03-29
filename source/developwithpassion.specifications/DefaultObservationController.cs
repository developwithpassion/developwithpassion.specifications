using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using developwithpassion.specifications.core;
using developwithpassion.specifications.dsl.fieldswitching;
using developwithpassion.specifications.faking;
using Machine.Specifications;

namespace developwithpassion.specifications
{
    public class DefaultObservationController<Class, Engine> : ObservationController<Class>
        where Class : class
    {
        Action because_behaviour;
        Exception exception_that_was_thrown;
        internal IManageFakes fakes_controller;
        ICreateAndManageDependenciesFor<Class> factory;
        public TestStateFor<Class> test_state { get; private set; }

        public DefaultObservationController(IManageFakes fakes_controller, TestStateFor<Class> test_state,
                                            ICreateAndManageDependenciesFor<Class> factory)
        {
            this.fakes_controller = fakes_controller;
            this.test_state = test_state;
            this.factory = factory;
        }

        public void catch_exception(Action behaviour_to_trigger)
        {
            this.because_behaviour = behaviour_to_trigger;
        }

        public void catch_exception<T>(Func<IEnumerable<T>> behaviour)
        {
            this.because_behaviour = () => behaviour().Count();
        }

        public ChangeExpression change(Expression<Func<object>> expression)
        {
            return new ChangeExpression(test_state.add_setup_teardown_pair, expression);
        }

        Exception get_exception_thrown_by(Action action)
        {
            return Catch.Exception(action);
        }

        public Class run_setup()
        {
            return this.test_state.run_setup();
        }

        public void run_tear_down()
        {
            this.test_state.run_tear_down();
        }

        public Exception exception_thrown
        {
            get
            {
                return this.exception_that_was_thrown ??
                    (this.exception_that_was_thrown = this.get_exception_thrown_by(this.because_behaviour));
            }
        }

        public IConfigureSetupPairs pipeline
        {
            get{ return this.test_state;}
        }
        
        public IConfigureTheSut<Class> sut_setup
        {
           get{ return this.test_state;} 
        }

        public ICreateFakes fake
        {
            get { return this.fakes_controller; }
        }

        public IConfigureSpecifications spec
        {
            get { return this; }
        }

        public IProvideDependencies depends
        {
            get { return factory; }
        }

        public SUTFactory<Class> sut_factory
        {
            get { return factory;}
        }
    }
}