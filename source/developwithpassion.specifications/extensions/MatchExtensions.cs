using developwithpassion.specifications.core;

namespace developwithpassion.specifications.extensions
{
    public static class MatchExtensions
    {
        public static IMatchAnItem<ItemToMatch> not<ItemToMatch>(this IMatchAnItem<ItemToMatch> item)
        {
            return new NegatingMatcher<ItemToMatch>(item);
        }
    }
}