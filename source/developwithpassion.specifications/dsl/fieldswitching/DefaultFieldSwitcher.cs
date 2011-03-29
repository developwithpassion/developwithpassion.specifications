namespace developwithpassion.specifications.dsl.fieldswitching
{
public class DefaultFieldSwitcher : FieldSwitcher
{
    MemberTarget member_target;
    object original_value;

    public DefaultFieldSwitcher(MemberTarget member_target)
    {
        this.member_target = member_target;
        this.original_value = member_target.get_value();
    }

    public SetupTearDownPair to(object new_value)
    {
        return new SetupTearDownPair(() => member_target.change_value_to(new_value),
            () => member_target.change_value_to(original_value));
    }
}
 
 
}