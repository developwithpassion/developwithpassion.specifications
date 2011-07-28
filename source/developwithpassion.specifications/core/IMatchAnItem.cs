namespace developwithpassion.specifications.core
{
    public interface IMatchAnItem<ItemToMatch>
    {
        bool matches(ItemToMatch item);
    }
}