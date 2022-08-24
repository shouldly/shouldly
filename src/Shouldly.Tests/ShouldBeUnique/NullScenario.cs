namespace Shouldly.Tests.ShouldBeUnique;

public class NullScenario
{
    [Fact]
    public void NullShouldBeDetectedAsADuplicate()
    {
        Verify.ShouldFail(() =>
                new string?[] { null, null }.ShouldBeUnique("Some additional context"),

            errorWithSource:
            @"new string?[] { null, null }
    should be unique but
[null]
    was duplicated

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[null, null]
    should be unique but
[null]
    was duplicated

Additional Info:
    Some additional context");
    }

    [Fact]
    public void NullShouldBeDetectedAsADuplicateWhenUsingComparer()
    {
        Verify.ShouldFail(() =>
                new string?[] { null, null }.ShouldBeUnique(StringComparer.OrdinalIgnoreCase, "Some additional context"),

            errorWithSource:
            @"new string?[] { null, null }
    should be unique but
[null]
    was duplicated

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[null, null]
    should be unique but
[null]
    was duplicated

Additional Info:
    Some additional context");
    }
}