namespace Shouldly.Tests.Strings;

public class ShouldMatch
{
    [Fact]
    public void ShouldMatchShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldMatch(@"\d+", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldMatch("C.e{2}s[e]");
    }
}