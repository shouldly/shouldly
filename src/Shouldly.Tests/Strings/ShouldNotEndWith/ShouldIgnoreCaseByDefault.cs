namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class ShouldIgnoreCaseByDefault
{
    [Fact]
    public void ShouldIgnoreCaseByDefaultShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("SE", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("ze");
    }
}