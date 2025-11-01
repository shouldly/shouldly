namespace Shouldly.Tests.ShouldBeSupersetOf;

public class DecimalArrayScenario
{
    [Fact]
    public void DecimalArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 2m, 3m, 4m }.ShouldBeSupersetOf([1m, 2m, 5m], "Some additional context"),

            errorWithSource:
            """
            new[] { 2m, 3m, 4m }
                should be superset of
            [1m, 2m, 5m]
                but
            [1m, 5m]
                are outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [2m, 3m, 4m]
                should be superset of
            [1m, 2m, 5m]
                but
            [1m, 5m]
                are outside superset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1m, 2m, 3m }.ShouldBeSupersetOf([1m]);
    }
}