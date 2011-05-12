namespace developwithpassion.specifications.core
{
  public interface IConfigureTheSut<Contract>
  {
    void run(SUTContextSetup<Contract> action);
  }
}