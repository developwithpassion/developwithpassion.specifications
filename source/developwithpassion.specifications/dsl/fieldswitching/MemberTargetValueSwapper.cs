namespace developwithpassion.specifications.dsl.fieldswitching
{
public class MemberTargetValueSwapper : ISwapValues
{
    MemberTarget member_target;
    object original_value;

    public MemberTargetValueSwapper(MemberTarget member_target)
    {
        this.member_target = member_target;
        this.original_value = member_target.get_value();
    }

    public ObservationPair to(object new_value)
    {
        return new ObservationPair(() => member_target.change_value_to(new_value),
            () => member_target.change_value_to(original_value));
    }
}
 
 
}