namespace Shouldly.Tests.ShouldBeSubsetOf;

public class DecimalArrayScenario
{
    [Fact]
    public void DecimalArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1m, 2m, 5m }.ShouldBeSubsetOf([2m, 3m, 4m], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1m }.ShouldBeSubsetOf([1m, 2m, 3m]);
    }
}