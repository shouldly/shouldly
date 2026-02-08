namespace Shouldly.Tests.ShouldBeLessThan;

public class StringVsNullScenario
{
    [Fact]
    public void StringVsNullScenarioShouldFail()
    {
        var bee = "b";
        Verify.ShouldFail(() =>
            bee.ShouldBeLessThan(null, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ((string?)null).ShouldBeLessThan("b");
    }
}