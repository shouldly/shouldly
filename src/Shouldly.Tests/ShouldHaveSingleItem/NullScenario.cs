namespace Shouldly.Tests.ShouldHaveSingleItem;

public class NullScenario
{
    [Fact]
    public void NullScenarioShouldFail()
    {
        string[]? nullableCollection = null;
        Verify.ShouldFail(() =>
                nullableCollection.ShouldHaveSingleItem("Some additional context"),

            errorWithSource:
            """
            nullableCollection
                should have single item but was
            null

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            null
                should have single item

            Additional Info:
                Some additional context
            """);
    }
}
