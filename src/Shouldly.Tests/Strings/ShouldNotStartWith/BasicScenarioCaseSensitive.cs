namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class BasicScenarioCaseSensitive
{
    [Fact]
    public void BasicScenarioCaseSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotStartWith("Ch", Case.Sensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("CH", Case.Sensitive);
    }
}