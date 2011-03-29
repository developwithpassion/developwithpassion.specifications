using System;

namespace developwithpassion.specifications.faking
{
    public interface IMarshalNonGenericFakeResolutionToAGenericResolution
    {
        object resolve(Type item);
    }
}