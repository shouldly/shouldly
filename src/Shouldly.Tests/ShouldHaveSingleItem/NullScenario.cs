namespace Shouldly.Tests.ShouldHaveSingleItem;

public class NullScenario
{
    [Fact]
    public void NullScenarioShouldFail()
    {
        string[]? nullableCollection = null;
        Verify.ShouldFail(() =>
            nullableCollection.ShouldHaveSingleItem("Some additional context"));
    }
}
