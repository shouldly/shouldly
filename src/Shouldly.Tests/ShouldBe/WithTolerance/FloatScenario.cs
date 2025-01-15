namespace Shouldly.Tests.ShouldBe.WithTolerance;

public static class MathEx
{
    public const double PI = 3.14159;
}

public class FloatScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FloatScenarioShouldFail()
    {
        const float pi = (float)MathEx.PI;
        Verify.ShouldFail(() =>
                pi.ShouldBe(3.24f, 0.01d, "Some additional context"),

            errorWithSource:
            """
            pi
                should be within
            0.01d
                of
            3.24f
                but was
            3.14159f

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            3.14159f
                should be within
            0.01d
                of
            3.24f
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        const float pi = (float)MathEx.PI;
        pi.ShouldBe(3.14f, 0.01d);
    }
}