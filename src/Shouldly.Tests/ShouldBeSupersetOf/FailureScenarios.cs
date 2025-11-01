namespace Shouldly.Tests.ShouldBeSupersetOf;

public sealed class FailureScenarios
{
    [Fact]
    public void EmptyArrayIsNotSupersetOfAnything()
    {
        Verify.ShouldFail(() =>
            new int[0].ShouldBeSupersetOf([1], "Some additional context"),
            
            errorWithSource:
            """
            new int[0]
                should be superset of
            [1]
                but
            [1]
                is outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            []
                should be superset of
            [1]
                but
            [1]
                is outside superset

            Additional Info:
                Some additional context
            """);
        
    }
}