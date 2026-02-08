namespace Shouldly.Tests.Strings.ShouldStartWith;

public class ShouldIgnoreCaseByDefault
{
    [Fact]
    public void ShouldIgnoreCaseByDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldStartWith("Ce"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldStartWith("CH");
    }
}