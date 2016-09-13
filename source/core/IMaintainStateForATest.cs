namespace developwithpassion.specifications.core
{
  public interface IMaintainStateForATest<SUT> :
    IConfigureTheSut<SUT>, IConfigureTheTestStateFor<SUT>, IConfigureSetupPairs
  {
  }
}