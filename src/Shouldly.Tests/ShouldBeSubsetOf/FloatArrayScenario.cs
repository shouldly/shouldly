namespace Shouldly.Tests.ShouldBeSubsetOf;

public class FloatArrayScenario
{
    [Fact]
    public void FloatArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1f, 2f, 5f }.ShouldBeSubsetOf([2f, 3f, 4f], "Some additional context"),

            errorWithSource:
            """
            new[] { 1f, 2f, 5f }
                should be subset of
            [2f, 3f, 4f]
                but
            [1f, 5f]
                are outside subset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1f, 2f, 5f]
                should be subset of
            [2f, 3f, 4f]
                but
            [1f, 5f]
                are outside subset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1f }.ShouldBeSubsetOf([1f, 2f, 3f]);
    }
}