using Machine.Fakes;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheMainObservationController
    {
        ObservationController<Class> create_main_controller<Class, Engine>() where Class : class
            where Engine : IFakeEngine, new();
    }
}