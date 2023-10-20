namespace Shouldly.Tests.Strings;

public class ShouldMatch
{
    [Fact]
    public void ShouldMatchShouldFail()
    {
        Verify.ShouldFail(() =>
                "Cheese".ShouldMatch(@"\d+", "Some additional context"),

            errorWithSource:
            """
            "Cheese"
                should match
            "\d+"
                but was not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "Cheese"
                should match
            "\d+"
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldMatch(@"C.e{2}s[e]");
    }
}