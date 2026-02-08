namespace Shouldly.Tests.ShouldBeOfType;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeOfType<string>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeOfType<int>();
    }
}