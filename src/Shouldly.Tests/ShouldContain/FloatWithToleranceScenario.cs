namespace Shouldly.Tests.ShouldContain;

public class FloatWithToleranceScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FloatWithToleranceScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1f, 2f, 3f }.ShouldContain(1.8f, 0.1d, "Some additional context"),

            errorWithSource:
            """
            new[] { 1f, 2f, 3f }
                should contain
            1.8f
                within
            0.1d
                but was
            [1f, 2f, 3f]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1f, 2f, 3f]
                should contain
            1.8f
                within
            0.1d
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1f, 2f, 3f }.ShouldContain(1.91f, 0.1d);
    }
}