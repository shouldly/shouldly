namespace Shouldly.Tests.ShouldNotBeAssignableTo;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var two = 2;
        Verify.ShouldFail(() =>
            two.ShouldNotBeAssignableTo<int>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBeAssignableTo<string>();
    }
}