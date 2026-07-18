namespace Shouldly.Tests.ShouldNotContain;

public class IntegerWithNegativeValuesScenario
{
    private readonly int[] _target = [2, 3, 4, 5, 4, 123665, 11234, -13562377];

    [Fact]
    public void IntegerWithNegativeValuesScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _target.ShouldNotContain(3, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain(-300);
    }
}