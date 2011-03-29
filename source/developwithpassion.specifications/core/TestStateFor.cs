namespace developwithpassion.specifications.core
{
    public interface TestStateFor<SUT> : 
        IConfigureTheSut<SUT>, IConfigureTheTestStateFor<SUT>, IConfigureSetupPairs
    {
    }
}