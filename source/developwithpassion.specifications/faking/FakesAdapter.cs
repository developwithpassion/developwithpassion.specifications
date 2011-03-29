using System;
using developwithpassion.specifications.core;
using Machine.Fakes;

namespace developwithpassion.specifications.faking
{
    public class FakesAdapter : IManageFakes, ICreateFakes
    {
        IFakeAccessor original_controller;

        public FakesAdapter(IFakeAccessor original_controller)
        {
            this.original_controller = original_controller;
        }

        public InterfaceType an<InterfaceType>() where InterfaceType : class
        {
            return (InterfaceType) this.an(typeof(InterfaceType));
        }

        public object an(Type type)
        {
            return this.original_controller.An(type);
        }

        public Dependency the<Dependency>() where Dependency : class
        {
            return this.original_controller.The<Dependency>();
        }

        public void use<Dependency>(Dependency value)
        {
            this.original_controller.Use(value);
        }
    }
}