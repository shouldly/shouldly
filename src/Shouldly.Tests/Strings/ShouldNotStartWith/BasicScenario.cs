namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            "Cheese".ShouldNotStartWith("Ch", customMessage: "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("Ce");
    }
}