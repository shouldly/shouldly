namespace Shouldly.Tests.ShouldBeSupersetOf;

public class StringArrayScenario
{
    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "1", null }.ShouldBeSupersetOf(["1", "2", null, "2", "3"], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "1", "2", null }.ShouldBeSupersetOf(["1", null]);
    }
}