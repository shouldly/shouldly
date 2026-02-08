namespace Shouldly.Tests.Strings.ShouldStartWith;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldStartWith("Ce", customMessage: "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldStartWith("Ch");
    }
}