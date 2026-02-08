namespace Shouldly.Tests.ShouldNotContain;

public class PredicateScenario
{
    [Fact]
    public void PredicateScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldNotContain(i => i < 4, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldNotContain(i => i > 3);
    }
}