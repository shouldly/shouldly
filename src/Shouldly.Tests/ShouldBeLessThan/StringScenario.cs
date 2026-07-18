namespace Shouldly.Tests.ShouldBeLessThan;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var beeeee = "b";
        Verify.ShouldFail(() =>
            beeeee.ShouldBeLessThan("a", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "a".ShouldBeLessThan("b");
    }
}