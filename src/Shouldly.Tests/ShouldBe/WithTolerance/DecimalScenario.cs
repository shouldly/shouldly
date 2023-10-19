namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class DecimalScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DecimalScenarioShouldFail()
    {
        const decimal pi = (decimal)MathEx.PI;
        Verify.ShouldFail(() =>
                pi.ShouldBe(3.24m, 0.01m, "Some additional context"),

            errorWithSource:
            """
            pi
                should be within
            0.01m
                of
            3.24m
                but was
            3.14159m

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            3.14159m
                should be within
            0.01m
                of
            3.24m
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        const decimal pi = (decimal)MathEx.PI;
        pi.ShouldBe(3.14m, 0.01m);
    }
}