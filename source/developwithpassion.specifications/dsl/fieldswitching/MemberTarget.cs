namespace developwithpassion.specifications.dsl.fieldswitching
{
  public interface MemberTarget
  {
    void change_value_to(object new_value);
    object get_value();
  }
}