namespace Shouldly.Tests.ShouldBeSupersetOf;

public class FloatArrayScenario
{
    [Fact]
    public void FloatArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 2f, 3f, 4f }.ShouldBeSupersetOf([1f, 2f, 5f], "Some additional context"),

            errorWithSource:
            """
            new[] { 2f, 3f, 4f }
                should be superset of
            [1f, 2f, 5f]
                but
            [1f, 5f]
                are outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [2f, 3f, 4f]
                should be superset of
            [1f, 2f, 5f]
                but
            [1f, 5f]
                are outside superset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1f, 2f, 3f }.ShouldBeSupersetOf([1f]);
    }
}