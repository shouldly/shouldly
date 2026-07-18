namespace Shouldly.Tests.ShouldBeAssignableTo;

public class BasicTypesScenario
{
    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        var two = 2;
        Verify.ShouldFail(() =>
            two.ShouldBeAssignableTo<double>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeAssignableTo<int>();
    }
}