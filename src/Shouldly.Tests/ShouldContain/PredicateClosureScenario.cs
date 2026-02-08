namespace Shouldly.Tests.ShouldContain;

public class PredicateClosureScenario
{
    [Fact]
    public void PredicateClosureScenarioShouldFail()
    {
        var capturedOuterVar = 4;
        int[] arr = [1, 2, 3];
        Verify.ShouldFail(() =>
            arr.ShouldContain(i => i > capturedOuterVar, "Some additional context"));
    }
}