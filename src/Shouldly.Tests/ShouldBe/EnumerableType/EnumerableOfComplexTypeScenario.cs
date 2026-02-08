namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class EnumerableOfComplexTypeScenario
{
    private readonly IEnumerable<Widget> _aEnumerable = new Widget { Name = "Joe", Enabled = true }.ToEnumerable();
    private readonly Widget[] _bArray = [new() { Name = "Joeyjojoshabadoo Jr", Enabled = true }];

    [Fact]
    public void EnumerableOfComplexTypeScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            _aEnumerable.ShouldBe(_bArray, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        _aEnumerable.ShouldBe(_aEnumerable);
    }
}