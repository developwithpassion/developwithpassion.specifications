using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications.faking
{
    public class DefaultSUTFactory<SUT> : ICreateAndManageDependenciesFor<SUT>
    {
        public CreateSUT<SUT> actual_factory;
        IMarshalNonGenericFakeResolutionToAGenericResolution fake_resolver;
        IDictionary<Type, object> specific_constructor_arguments;
        IManageFakes fake_gateway;

        public DefaultSUTFactory(IDictionary<Type, object> specific_constructor_arguments,IMarshalNonGenericFakeResolutionToAGenericResolution
                                     fake_resolver, IManageFakes fake_gateway)
        {
            this.actual_factory = new CreateSUT<SUT>(this.create_manually);
            this.specific_constructor_arguments = specific_constructor_arguments;
            this.fake_gateway = fake_gateway;
            this.fake_resolver =
                fake_resolver;
        }

        public SUT create()
        {
            return this.actual_factory();
        }

        public SUT create_manually()
        {
            var greediest_constructor = typeof(SUT).greediest_constructor();
            var constructor_parameters = greediest_constructor.GetParameters().Select(x => get_constructor_parameter(x.ParameterType));
            return (SUT) greediest_constructor.Invoke(constructor_parameters.ToArray());
        }

        public void create_using(CreateSUT<SUT> specific_factory)
        {
            this.actual_factory = specific_factory;
        }

        public Dependency on<Dependency>() where Dependency : class
        {
            return fake_gateway.the<Dependency>();
        }

        public void on<ArgumentType>(ArgumentType value)
        {
            specific_constructor_arguments.Add(typeof(ArgumentType),value);
            fake_gateway.use(value);
        }

        public void dependency<ArgumentType>(ArgumentType value)
        {
            this.specific_constructor_arguments.Add(typeof(ArgumentType), value);
        }

        object get_constructor_parameter(Type parameter_type)
        {
            return (this.specific_constructor_arguments.ContainsKey(parameter_type)
                ? this.specific_constructor_arguments[parameter_type]
                : this.fake_resolver.resolve(parameter_type));
        }
    }
}