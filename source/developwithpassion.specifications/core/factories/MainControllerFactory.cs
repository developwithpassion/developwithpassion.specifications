using developwithpassion.specifications.faking;
using Machine.Fakes;

namespace developwithpassion.specifications.core.factories
{
public class MainControllerFactory : ICreateTheMainObservationController
{
    private ICreateANonGenericToGenericFakeAdapter fakes_adapter_factory;
    private ICreateTheFakesGateway fakes_gateway_factory;
    private ICreateTheFactoryThatCreatesTheSUT sut_factory_provider;
    private ICreateTheTestState test_state_factory;

    public  MainControllerFactory(ICreateTheFakesGateway fakes_gateway_factory, ICreateANonGenericToGenericFakeAdapter fakes_adapter_factory, ICreateTheFactoryThatCreatesTheSUT sut_factory_provider, ICreateTheTestState test_state_factory)
    {
        this.fakes_gateway_factory = fakes_gateway_factory;
        this.test_state_factory = test_state_factory;
        this.sut_factory_provider = sut_factory_provider;
        this.fakes_adapter_factory = fakes_adapter_factory;
    }

    public ObservationController<Class> create_main_controller<Class, Engine>() where Class: class where Engine: IFakeEngine, new()
    {
        IManageFakes fakes_accessor = this.fakes_gateway_factory.create<Class, Engine>();
        IResolveADependencyForTheSUT fakes_resolver = this.fakes_adapter_factory.create(fakes_accessor);
        ICreateAndManageDependenciesFor<Class> sut_factory = this.sut_factory_provider.create<Class>(fakes_resolver,fakes_accessor);

        return new DefaultObservationController<Class, Engine>(fakes_accessor, test_state_factory.create_for(sut_factory),
            sut_factory);
    }

    public static ICreateTheMainObservationController new_instance()
    {
        return new MainControllerFactory(new FakeGatewayFactory(), new FakesAdapterFactory(), new SUTFactoryProvider(), new TestStateFactory());
    }
}

 
 
}