namespace Shouldly.Tests.ShouldBeSubsetOf;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 5 }.ShouldBeSubsetOf([2, 3, 4], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1 }.ShouldBeSubsetOf([1, 2, 3]);
    }
}