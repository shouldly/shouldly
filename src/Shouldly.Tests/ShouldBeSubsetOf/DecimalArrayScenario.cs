namespace Shouldly.Tests.ShouldBeSubsetOf;

public class DecimalArrayScenario
{
    [Fact]
    public void DecimalArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1m, 2m, 5m }.ShouldBeSubsetOf([2m, 3m, 4m], "Some additional context"),

            errorWithSource:
            """
            new[] { 1m, 2m, 5m }
                should be subset of
            [2m, 3m, 4m]
                but
            [1m, 5m]
                are outside subset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1m, 2m, 5m]
                should be subset of
            [2m, 3m, 4m]
                but
            [1m, 5m]
                are outside subset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1m }.ShouldBeSubsetOf([1m, 2m, 3m]);
    }
}