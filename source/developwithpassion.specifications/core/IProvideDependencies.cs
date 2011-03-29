namespace developwithpassion.specifications.core
{
    public interface IProvideDependencies
    {
        Dependency on<Dependency>() where Dependency : class;
        void on<ArgumentType>(ArgumentType value);
    }
}