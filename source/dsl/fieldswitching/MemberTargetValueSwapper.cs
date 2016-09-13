using developwithpassion.specifications.core.reflection;

namespace developwithpassion.specifications.dsl.fieldswitching
{
  public class MemberTargetValueSwapper : ISwapValues
  {
    MemberAccessor member_accessor;
    object original_value;

    public MemberTargetValueSwapper(MemberAccessor member_accessor)
    {
      this.member_accessor = member_accessor;
      this.original_value = member_accessor.get_value(member_accessor.declaring_type);
    }

    public ObservationPair to(object new_value)
    {
      return new ObservationPair(() => member_accessor.change_value_to(member_accessor.declaring_type, new_value),
        () => member_accessor.change_value_to(member_accessor.declaring_type, original_value));
    }
  }
}