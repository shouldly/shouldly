namespace Shouldly.Tests.ShouldBeUnique;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 2 }.ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldBeUnique();
    }
}