namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class ShouldIgnoreCaseByDefault
{
    [Fact]
    public void ShouldIgnoreCaseByDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotStartWith("cH", customMessage: "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("Ce");
    }
}