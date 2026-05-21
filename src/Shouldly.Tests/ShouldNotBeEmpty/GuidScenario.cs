namespace Shouldly.Tests.ShouldNotBeEmpty;

public class GuidScenario
{
    [Fact]
    public void GuidScenarioShouldFail()
    {
        var myGuidEmpty = Guid.Empty;

        Verify.ShouldFail(() =>
            myGuidEmpty.ShouldNotBeEmpty("Some additional context"),

            errorWithSource:
            """
            myGuidEmpty
                should not be empty
            00000000-0000-0000-0000-000000000000
                but was
            00000000-0000-0000-0000-000000000000

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            00000000-0000-0000-0000-000000000000
                should not be empty
            00000000-0000-0000-0000-000000000000
                but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var myGuid = Guid.NewGuid();
        myGuid.ShouldNotBeEmpty();
    }
}
