namespace Shouldly.Tests.ShouldBeSupersetOf;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldBeSupersetOf([2, 3, 4], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldBeSupersetOf([1, 2]);
    }
}
