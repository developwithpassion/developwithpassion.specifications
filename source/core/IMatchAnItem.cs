namespace developwithpassion.specifications.core
{
  public interface IMatchAnItem<in ItemToMatch>
  {
    bool matches(ItemToMatch item);
  }
}