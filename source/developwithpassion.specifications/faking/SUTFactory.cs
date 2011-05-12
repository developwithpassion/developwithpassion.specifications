namespace developwithpassion.specifications.faking
{
  public interface SUTFactory<SUT>
  {
    void create_using(CreateSUT<SUT> factory);
  }
}