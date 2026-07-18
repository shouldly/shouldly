namespace Shouldly.Tests.ShouldBeSameAs;

public class BoxedIntScenario
{
    private readonly object _boxedInt = 1;
    private readonly object _differentBoxedInt = 1;

    [Fact]
    public void BoxedIntScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _boxedInt.ShouldBeSameAs(_differentBoxedInt));
    }

    [Fact]
    public void ShouldPass()
    {
        _boxedInt.ShouldBeSameAs(_boxedInt);
    }
}