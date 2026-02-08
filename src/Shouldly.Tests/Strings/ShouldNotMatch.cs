namespace Shouldly.Tests.Strings;

public class ShouldNotMatch
{
    [Fact]
    public void ShouldNotMatchShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotMatch(@"\w+", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotMatch("Cat");
    }
}