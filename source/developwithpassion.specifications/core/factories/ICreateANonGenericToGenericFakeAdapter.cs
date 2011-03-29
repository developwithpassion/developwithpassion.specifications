using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public interface ICreateANonGenericToGenericFakeAdapter
    {
        IMarshalNonGenericFakeResolutionToAGenericResolution create(IManageFakes fakes_gateway);
    }
}