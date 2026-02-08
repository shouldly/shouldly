namespace Shouldly.Tests.Strings.ShouldStartWith;

public class BasicScenarioCaseInSensitive
{
    [Fact]
    public void BasicScenarioCaseInSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldStartWith("Ce", Case.Insensitive, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldStartWith("CH");
    }
}