using System;

namespace developwithpassion.specifications.faking
{
    public interface SUTFactory<SUT>
    {
        void create_using(Func<SUT> factory);
        void during_create(Func<Func<SUT>, SUT> sut_create_wrapper);
    }
}