namespace Shouldly.Tests.ShouldNotBeOfType;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldNotBeOfType<int>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBeOfType<string>();
    }
}