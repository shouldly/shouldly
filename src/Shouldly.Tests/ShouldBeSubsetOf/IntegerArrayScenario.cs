namespace Shouldly.Tests.ShouldBeSubsetOf;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 2, 5 }.ShouldBeSubsetOf([2, 3, 4], "Some additional context"),

            errorWithSource:
            """
            new[] { 1, 2, 5 }
                should be subset of
            [2, 3, 4]
                but
            [1, 5]
                are outside subset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 5]
                should be subset of
            [2, 3, 4]
                but
            [1, 5]
                are outside subset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1 }.ShouldBeSubsetOf([1, 2, 3]);
    }
}