using developwithpassion.specifications.faking;

namespace developwithpassion.specifications.core.factories
{
    public class FakesAdapterFactory : ICreateANonGenericToGenericFakeAdapter
    {
        public IMarshalNonGenericFakeResolutionToAGenericResolution create(IManageFakes fakes_gateway)
        {
            return new NonGenericFakesAdapter(fakes_gateway);
        }
    }
}