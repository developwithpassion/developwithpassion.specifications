using System;

namespace developwithpassion.specifications.core
{
    public interface IProvideDependencies
    {
        Dependency on<Dependency>() where Dependency : class;
        Dependency on<Dependency>(Dependency value);
        Dependency on<Dependency>(Dependency value, string name);
    }
}