namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class EnumerableOfDoubleScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfDoubleScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.24, 3.24], 0.01, "Some additional context"),

            errorWithSource:
            """
            new[] { MathEx.PI, MathEx.PI }
                should be within
            0.01d
                of
            [3.24d, 3.24d]
                but was
            [3.14159d, 3.14159d]
                difference
            [*3.14159d*, *3.14159d*]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [3.14159d, 3.14159d]
                should be within
            0.01d
                of
            [3.24d, 3.24d]
                but was not
                difference
            [*3.14159d*, *3.14159d*]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.14, 3.14], 0.01);
    }
}