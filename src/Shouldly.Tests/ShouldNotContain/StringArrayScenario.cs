namespace Shouldly.Tests.ShouldNotContain;

public class StringArrayScenario
{
    private readonly string[] _target = ["a", "b", "c"];

    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _target.ShouldNotContain("c", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain("d");
    }
}