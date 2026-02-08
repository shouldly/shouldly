namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class BasicScenarioCaseInSensitive
{
    [Fact]
    public void BasicScenarioCaseInSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotStartWith("cH", Case.Insensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("Ce");
    }
}