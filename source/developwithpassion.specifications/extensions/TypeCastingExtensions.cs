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
      try
      {
        var type_to_cast_to = (T) item;
        return false;
      }
      catch
      {
        return true;
      }
    }
  }
}