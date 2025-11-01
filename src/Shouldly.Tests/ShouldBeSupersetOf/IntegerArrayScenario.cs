namespace Shouldly.Tests.ShouldBeSupersetOf;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 2, 3, 4 }.ShouldBeSupersetOf([1, 2, 5], "Some additional context"),

            errorWithSource:
            """
            new[] { 2, 3, 4 }
                should be superset of
            [1, 2, 5]
                but
            [1, 5]
                are outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [2, 3, 4]
                should be superset of
            [1, 2, 5]
                but
            [1, 5]
                are outside superset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldBeSupersetOf([1]);
    }
}