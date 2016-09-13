namespace developwithpassion.specifications.core
{
  public interface IManageFakes : ICreateFakes
  {
    Dependency the<Dependency>() where Dependency : class;
    void use<Dependency>(Dependency value);
  }
}