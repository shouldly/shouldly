namespace Shouldly.Tests.ShouldAllBe;

public class TypeBinaryExpressionScenario
{
    [Fact]
    public void TypeBinaryExpressionScenarioShouldFail()
    {
        var objects = new List<object> { "1", 1 };

        Verify.ShouldFail(() =>
                objects.ShouldAllBe(x => x is string, "Some additional context"),

            errorWithSource:
            """
            objects
                should satisfy the condition
            (x Is String)
                but
            [1]
                do not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            ["1", 1]
                should satisfy the condition
            (x Is String)
                but
            [1]
                do not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "1", "2", "3" }.ShouldAllBe(x => x is string);
    }
}