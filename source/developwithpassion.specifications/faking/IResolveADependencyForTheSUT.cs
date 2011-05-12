using System;

namespace developwithpassion.specifications.faking
{
  public interface IResolveADependencyForTheSUT
  {
    object resolve(Type item);
  }
}