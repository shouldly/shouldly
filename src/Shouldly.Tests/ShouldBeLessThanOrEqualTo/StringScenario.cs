namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var bee = "b";
        Verify.ShouldFail(() =>
            bee.ShouldBeLessThanOrEqualTo("a", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "a".ShouldBeLessThanOrEqualTo("a");
    }
}