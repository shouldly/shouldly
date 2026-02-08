namespace Shouldly.Tests.Strings.ShouldStartWith;

public class BasicScenarioCaseSensitive
{
    [Fact]
    public void BasicScenarioCaseSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldStartWith("cH", Case.Sensitive));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldStartWith("Ch", Case.Sensitive);
    }
}