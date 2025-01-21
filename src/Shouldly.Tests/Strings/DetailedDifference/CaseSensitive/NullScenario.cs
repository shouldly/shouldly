namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public static class NullScenario
{
    [Fact]
    public static void ShouldNotShowDifferenceWhenActualIsMissing()
    {
        var str = (string?)null;
        Verify.ShouldFail(() =>
                str.ShouldBe("null"),

            errorWithSource:
            """
            str
                should be
            "null"
                but was
            null
            """,

            errorWithoutSource:
            """
            null
                should be
            "null"
                but was not
            """);
    }

    [Fact]
    public static void ShouldNotShowDifferenceWhenExpectedIsMissing()
    {
        var str = "null";
        Verify.ShouldFail(() =>
                str.ShouldBe(null),

            errorWithSource:
            """
            str
                should be
            null
                but was
            "null"
            """,

            errorWithoutSource:
            """
            "null"
                should be
            null
                but was not
            """);
    }
}