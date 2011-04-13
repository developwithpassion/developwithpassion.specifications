using System;

namespace developwithpassion.specifications.faking
{
    public interface ICreateDelegatesThatThrowExceptions
    {
        object generate_delegate_for(Type delegate_type);
    }
}