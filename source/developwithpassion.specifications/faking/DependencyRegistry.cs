using System;
using System.Collections.Generic;
using System.Linq;
using developwithpassion.specifications.core;
using developwithpassion.specifications.extensions;

namespace developwithpassion.specifications.faking
{
    public interface IManageTheDependenciesForASUT :IProvideDependencies
    {
        object get_dependency_of(Type dependency_type, string name);
        object get_dependency_of(Type dependency_type);
        bool has_been_provided_an(Type dependency_type);
    }

    public class DependenciesRegistry<SUT> : IManageTheDependenciesForASUT
    {
        public IDictionary<Type, IDictionary<string, object>> explicit_dependencies = new Dictionary<Type, IDictionary<string, object>>();
        IResolveADependencyForTheSUT dependency_resolver;
        IManageFakes fake_gateway;

        public DependenciesRegistry(IResolveADependencyForTheSUT dependency_resolver,IManageFakes fake_gateway)
        {
            this.dependency_resolver = dependency_resolver;
            this.fake_gateway = fake_gateway;
        }

        public bool has_been_provided_an(Type dependency_type)
        {
            return explicit_dependencies.ContainsKey(dependency_type);
        }

        public object get_dependency_of(Type dependency_type)
        {
            return get_dependency_of(dependency_type, "");
        }

        public object get_dependency_of(Type dependency_type, string name)
        {
            return (this.explicit_dependencies.ContainsKey(dependency_type)
                ? this.get_explicit_dependency(dependency_type, name)
                : this.dependency_resolver.resolve(dependency_type));
        }

        public Dependency on<Dependency>() where Dependency : class
        {
            return on(fake_gateway.the<Dependency>());
        }

        public Dependency on<Dependency>(Dependency value)
        {
            add_explicit_dependency(typeof(Dependency), value, "");
            return value;
        }

        public Dependency on<Dependency>(Dependency value, string name)
        {
            add_explicit_dependency(typeof(Dependency), value, name);
            return value;
        }

        private object get_explicit_dependency(Type dependency_type, string name)
        {
            if(!this.explicit_dependencies[dependency_type].ContainsKey(name) 
                && !this.explicit_dependencies[dependency_type].ContainsKey(""))
            {
                throw new Exception(string.Format("You must specify dependency of type {0} and name {1}, use depends.on(value, name)", dependency_type, name));
            }
            
            if(this.explicit_dependencies[dependency_type].ContainsKey(name))
            {
                return this.explicit_dependencies[dependency_type][name];    
            }

            return this.explicit_dependencies[dependency_type][""];
        }

        private void add_explicit_dependency(Type dependency_type, object value, string name)
        {
            if(!this.explicit_dependencies.ContainsKey(dependency_type))
            {
                this.explicit_dependencies.Add(dependency_type, new Dictionary<string, object>());
            }

            if (this.explicit_dependencies[dependency_type].ContainsKey("")
                || (name == "" && this.explicit_dependencies[dependency_type].Count > 0))
            {
                throw new Exception(string.Format("To specify multiple {0}, use depends.on(value, name)", dependency_type));
            }

            this.explicit_dependencies[dependency_type].Add(name, value);
        }
    }
}