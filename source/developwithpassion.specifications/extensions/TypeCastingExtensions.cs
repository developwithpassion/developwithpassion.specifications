using developwithpassion.specifications.core;

namespace developwithpassion.specifications.extensions
{
    public static class TypeCastingExtensions
    {
        public static T downcast_to<T>(this object item)
        {
            return (T) item;
        }

        public static bool is_not_a<T>(this object item)
        {
            return BlockThat.ignores_exceptions(() =>
            {
                var target = item.downcast_to<T>();
                return false;
            }, true);
        }
    }
}