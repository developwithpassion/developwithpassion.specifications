namespace developwithpassion.specifications.core.reflection
{
    public class AccessorHasAValue:IMatchAnItem<MemberAccessor>
    {
        object target;

        public AccessorHasAValue(object target)
        {
            this.target = target;
        }

        public bool matches(MemberAccessor accessor)
        {
            return accessor.accessor_type.IsValueType || accessor.get_value(target) != null;
        }
    }
}