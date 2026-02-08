namespace Shouldly.Tests.ShouldNotContain;

public class IntegerScenario
{
    private readonly int[] _target = [1, 2, 3, 4, 5];

    [Fact]
    public void IntegerScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _target.ShouldNotContain(3, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain(7);
    }
}