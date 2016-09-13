using System;

namespace developwithpassion.specifications.core
{
  public interface ICreateFakes
  {
    InterfaceType an<InterfaceType>() where InterfaceType : class;
    object an(Type type);
  }
}