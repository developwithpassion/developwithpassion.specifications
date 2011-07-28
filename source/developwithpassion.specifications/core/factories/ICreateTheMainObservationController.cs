using Machine.Fakes;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateTheMainObservationController
    {
        ObservationController<Class> create_main_controller<Class, Engine>() where Class : class
            where Engine : IFakeEngine, new();
    }

    public class MainControllerFactory : ICreateTheMainObservationController
    {
        public ICreateANonGenericToGenericFakeAdapter fakes_adapter_factory { get; set; }
        public ICreateTheFakesGateway fakes_gateway_factory { get; set; }
        public ICreateTheFactoryThatCreatesTheSUT sut_factory_provider { get; set; }
        public ICreateTheTestState test_state_factory { get; set; }
        public ICreateTheDependencyRegistry dependency_registry_factory { get; set; }
        public ICreateTheNonCtorDependencyVisitor non_ctor_dependency_visitor_factory { get; set; }

        public ObservationController<Class> create_main_controller<Class, Engine>() where Class : class
            where Engine : IFakeEngine, new()
        {
            var fakes_accessor = this.fakes_gateway_factory.create<Class, Engine>();
            var fakes_resolver = this.fakes_adapter_factory.create(fakes_accessor);
            var dependency_registry = this.dependency_registry_factory.create(fakes_accessor,
                                                                              fakes_resolver);
            var sut_factory = this.sut_factory_provider.create<Class>(dependency_registry,
                                                                      non_ctor_dependency_visitor_factory.create(
                                                                          dependency_registry));

            return new DefaultObservationController<Class, Engine>(fakes_accessor,
                                                                   test_state_factory.create_for(sut_factory),
                                                                   sut_factory);
        }

        public static ICreateTheMainObservationController new_instance()
        {
            return new MainControllerFactory
            {
                fakes_adapter_factory = new FakesAdapterFactory(),
                fakes_gateway_factory = new FakeGatewayFactory(),
                sut_factory_provider = new SUTFactoryProvider(),
                test_state_factory = new TestStateFactory(),
                dependency_registry_factory = new DependencyRegistryFactory(),
                non_ctor_dependency_visitor_factory = new NonCtorDependencyVisitorFactory()
            };
        }
    }
}