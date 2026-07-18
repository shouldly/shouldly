namespace Shouldly.Tests.ShouldBeSubsetOf;

public class StringArrayScenario
{
    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "1", "2", "3" }.ShouldBeSubsetOf(["1", "2"], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "1", "2", "3" }.ShouldBeSubsetOf(["1", "2", "3", "4"]);
    }
}