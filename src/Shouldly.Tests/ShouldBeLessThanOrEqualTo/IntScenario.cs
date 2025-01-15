namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var seven = 7;
        Verify.ShouldFail(() =>
                seven.ShouldBeLessThanOrEqualTo(1, "Some additional context"),

            errorWithSource:
            """
            seven
                should be less than or equal to
            1
                but was
            7

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            7
                should be less than or equal to
            1
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeLessThanOrEqualTo(1);
    }
}