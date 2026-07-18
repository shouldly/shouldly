namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class ShouldBeCaseSensitiveByDefault
{
    [Fact]
    public void ShouldBeCaseSensitiveByDefaultShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("se", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("SE");
        "Cheese".ShouldNotEndWith("ze");
    }
}