namespace Shouldly.Tests.ShouldNotContain;

public class PredicateClosureScenario
{
    [Fact]
    public void PredicateClosureScenarioShouldFail()
    {
        var capturedOuterVar = 4;
        var arr = new[] { 1, 2, 3 };
        Verify.ShouldFail(() =>
            arr.ShouldNotContain(i => i < capturedOuterVar, "Some additional context"));
    }
}