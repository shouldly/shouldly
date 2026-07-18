namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenarioCaseSensitive
{
    [Fact]
    public void BasicScenarioCaseSensitiveShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("se", "Some additional context", Case.Sensitive));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("SE", Case.Sensitive);
    }
}