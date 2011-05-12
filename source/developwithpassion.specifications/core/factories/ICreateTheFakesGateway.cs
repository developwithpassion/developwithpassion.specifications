using Machine.Fakes;

namespace developwithpassion.specifications.core.factories
{
  public interface ICreateTheFakesGateway
  {
    IManageFakes create<Class, Engine>() where Class : class where Engine : IFakeEngine, new();
  }
}