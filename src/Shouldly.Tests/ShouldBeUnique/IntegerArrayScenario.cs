namespace Shouldly.Tests.ShouldBeUnique;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 2, 2 }.ShouldBeUnique("Some additional context"),

            errorWithSource:
            """
            new[] { 1, 2, 2 }
                should be unique but
            [2]
                was duplicated

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 2]
                should be unique but
            [2]
                was duplicated

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldBeUnique();
    }
}