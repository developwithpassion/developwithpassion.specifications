using System;
using System.Collections.Generic;
using developwithpassion.specifications.core;

namespace developwithpassion.specifications.faking
{
    public interface IManageTheDependenciesForASUT :IProvideDependencies
    {
        object get_dependency_of(Type dependency_type);
    }

    public class DependenciesRegistry : IManageTheDependenciesForASUT
    {
        public IDictionary<Type, object> explicit_dependencies = new Dictionary<Type, object>();
        IResolveADependencyForTheSUT dependency_resolver;
        IManageFakes fake_gateway;

        public DependenciesRegistry(IResolveADependencyForTheSUT dependency_resolver,IManageFakes fake_gateway)
        {
            this.dependency_resolver = dependency_resolver;
            this.fake_gateway = fake_gateway;
        }

        public object get_dependency_of(Type dependency_type)
        {
            return (this.explicit_dependencies.ContainsKey(dependency_type)
                ? this.explicit_dependencies[dependency_type]
                : this.dependency_resolver.resolve(dependency_type));
        }

        public Dependency on<Dependency>() where Dependency : class
        {
            return on(fake_gateway.the<Dependency>());
        }

        public Dependency on<Dependency>(Dependency value)
        {
            explicit_dependencies[typeof(Dependency)] = value;
            return value;
        }

    }
}