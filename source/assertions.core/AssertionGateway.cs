namespace developwithpassion.specifications.assertions.core
{
  public static class AssertionGateway
  {
    public static IProvideAccessToAssertions<ValueToAssertAgainst> should<ValueToAssertAgainst>(
      this ValueToAssertAgainst value)
    {
      return new AssertionExtensionPoint<ValueToAssertAgainst>(value);
    }
  }
}