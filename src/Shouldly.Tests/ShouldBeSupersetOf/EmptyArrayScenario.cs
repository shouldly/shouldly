namespace Shouldly.Tests.ShouldBeSupersetOf;

public class EmptyArrayScenario
{
    [Fact]
    public void EmptyArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new int[0].ShouldBeSupersetOf([1], "Some additional context"));
    }
}