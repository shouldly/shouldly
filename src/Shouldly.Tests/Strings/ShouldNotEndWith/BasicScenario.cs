namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
            str.ShouldNotEndWith("se", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("ze");
    }
}