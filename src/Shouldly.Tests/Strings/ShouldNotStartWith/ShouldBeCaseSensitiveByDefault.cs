namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class ShouldBeCaseSensitiveByDefault
{
    [Fact]
    public void ShouldBeCaseSensitiveByDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotStartWith("Ch", customMessage: "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("cH");
        "Cheese".ShouldNotStartWith("Ce");
    }
}