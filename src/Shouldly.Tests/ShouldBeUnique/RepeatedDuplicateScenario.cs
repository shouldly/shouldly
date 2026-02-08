namespace Shouldly.Tests.ShouldBeUnique;

public class RepeatedDuplicateScenario
{
    [Fact]
    public void EachDuplicateShouldBeReportedOnce()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 1, 2, 2, 2, 1 }.ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void EachDuplicateShouldBeReportedOnceWhenUsingComparer()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 1, 2, 2, 2, 1 }.ShouldBeUnique(EqualityComparer<int>.Default, "Some additional context"));
    }
}