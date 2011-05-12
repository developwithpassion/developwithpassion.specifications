using System;

namespace developwithpassion.specifications.faking
{
  public interface ICreateFakeDelegates
  {
    object generate_delegate_for(Type delegate_type);
  }
}