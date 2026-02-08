namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public static class NullScenario
{
    [Fact]
    public static void ShouldNotShowDifferenceWhenActualIsMissing()
    {
        var str = (string?)null;
        Verify.ShouldFail(() =>
            str.ShouldBe("null", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public static void ShouldNotShowDifferenceWhenExpectedIsMissing()
    {
        var str = "null";
        Verify.ShouldFail(() =>
            str.ShouldBe(null, StringCompareShould.IgnoreCase));
    }
}