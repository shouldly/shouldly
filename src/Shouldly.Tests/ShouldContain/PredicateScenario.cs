namespace Shouldly.Tests.ShouldContain;

public class PredicateScenario
{
    [Fact]
    public void PredicateScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldContain(i => i > 4, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldContain(i => i < 3);
    }
}