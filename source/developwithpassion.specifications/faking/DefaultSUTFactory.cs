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
        IResolveADependencyForTheSUT dependency_resolver;
        IDictionary<Type, object> explicit_constructor_parameters;
        IManageFakes fake_gateway;

        public DefaultSUTFactory(IDictionary<Type, object> explicit_constructor_parameters,IResolveADependencyForTheSUT
                                     dependency_resolver, IManageFakes fake_gateway)
        {
            this.actual_factory = new CreateSUT<SUT>(this.create_manually);
            this.explicit_constructor_parameters = explicit_constructor_parameters;
            this.fake_gateway = fake_gateway;
            this.dependency_resolver = dependency_resolver;
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
            explicit_constructor_parameters.Add(typeof(ArgumentType),value);
        }

        object get_constructor_parameter(Type parameter_type)
        {
            return (this.explicit_constructor_parameters.ContainsKey(parameter_type)
                ? this.explicit_constructor_parameters[parameter_type]
                : this.dependency_resolver.resolve(parameter_type));
        }
    }
}