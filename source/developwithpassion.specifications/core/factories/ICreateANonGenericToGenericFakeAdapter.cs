using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateANonGenericToGenericFakeAdapter
    {
        IResolveADependencyForTheSUT create(IManageFakes fakes_gateway);
    }
}