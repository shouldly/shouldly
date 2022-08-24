namespace Shouldly.Tests.ShouldBeUnique;

public class RepeatedDuplicateScenario
{
    [Fact]
    public void EachDuplicateShouldBeReportedOnce()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 1, 2, 2, 2, 1 }.ShouldBeUnique("Some additional context"),

            errorWithSource:
            @"new[] { 1, 1, 2, 2, 2, 1 }
    should be unique but
[1, 2]
    was duplicated

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1, 1, 2, 2, 2, 1]
    should be unique but
[1, 2]
    was duplicated

Additional Info:
    Some additional context");
    }

    [Fact]
    public void EachDuplicateShouldBeReportedOnceWhenUsingComparer()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 1, 2, 2, 2, 1 }.ShouldBeUnique(EqualityComparer<int>.Default, "Some additional context"),

            errorWithSource:
            @"new[] { 1, 1, 2, 2, 2, 1 }
    should be unique but
[1, 2]
    was duplicated

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1, 1, 2, 2, 2, 1]
    should be unique but
[1, 2]
    was duplicated

Additional Info:
    Some additional context");
    }
}