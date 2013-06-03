using System;

namespace developwithpassion.specifications.faking
{
    public interface SUTFactory<SUT>
    {
        void create_using(Func<SUT> factory);
    }
}