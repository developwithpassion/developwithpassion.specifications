using System;
using System.Linq;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications.faking
{
    public class DefaultSUTFactory<SUT> : ICreateAndManageDependenciesFor<SUT>
    {
        public Func<SUT> actual_factory;
        IManageTheDependenciesForASUT manage_the_dependencies_for_asut;
        IUpdateNonCtorDependenciesOnAnItem non_ctor_dependency_visitor;

        public DefaultSUTFactory(IManageTheDependenciesForASUT manage_the_dependencies_for_asut,
                                 IUpdateNonCtorDependenciesOnAnItem non_ctor_dependency_visitor)
        {
            this.actual_factory = create_automatically;
            this.manage_the_dependencies_for_asut = manage_the_dependencies_for_asut;
            this.non_ctor_dependency_visitor = non_ctor_dependency_visitor;
        }

        public SUT create()
        {
            return this.actual_factory();
        }

        SUT create_automatically()
        {
            var greediest_constructor = typeof(SUT).greediest_constructor();
            var constructor_parameters = greediest_constructor.GetParameters().Select(
                x => manage_the_dependencies_for_asut.get_dependency_of(x.ParameterType, x.Name));
            var the_sut = (SUT) greediest_constructor.Invoke(constructor_parameters.ToArray());
            non_ctor_dependency_visitor.update(the_sut);

            return the_sut;
        }

        public void create_using(Func<SUT> specific_factory)
        {
            this.actual_factory = specific_factory;
        }

        public Dependency on<Dependency>() where Dependency : class
        {
            return manage_the_dependencies_for_asut.on<Dependency>();
        }

        public Dependency on<Dependency>(Dependency value)
        {
            return manage_the_dependencies_for_asut.on(value);
        }

        public Dependency on<Dependency>(Dependency value, string name)
        {
            return manage_the_dependencies_for_asut.on(value, name);
        }
    }
}