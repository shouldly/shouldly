namespace Shouldly.Tests.ShouldBeUnique;

public class NullScenario
{
    [Fact]
    public void NullShouldBeDetectedAsADuplicate()
    {
        Verify.ShouldFail(() =>
            new string?[] { null, null }.ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void NullShouldBeDetectedAsADuplicateWhenUsingComparer()
    {
        Verify.ShouldFail(() =>
            new string?[] { null, null }.ShouldBeUnique(StringComparer.OrdinalIgnoreCase, "Some additional context"));
    }
}