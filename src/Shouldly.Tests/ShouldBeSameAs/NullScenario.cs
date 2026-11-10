namespace Shouldly.Tests.ShouldBeSameAs;

public class NullScenario
{
    [Fact]
    public void NullActualScenarioShouldFail()
    {
        object? nothing = null;
        Verify.ShouldFail(() =>
            nothing.ShouldBeSameAs(new List<int> { 1 }));
    }

    [Fact]
    public void NullExpectedScenarioShouldFail()
    {
        var list = new List<int> { 1 };
        Verify.ShouldFail(() =>
            list.ShouldBeSameAs(null));
    }
}
