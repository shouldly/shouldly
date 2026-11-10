namespace Shouldly.Tests.ShouldBeSameAs;

public class ThrowingEqualsScenario
{
    [Fact]
    public void ThrowingEqualsScenarioShouldFail()
    {
        var first = new ThrowsOnEquals();
        var second = new ThrowsOnEquals();
        Verify.ShouldFail(() =>
            first.ShouldBeSameAs(second));
    }

    private class ThrowsOnEquals
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("Equals should not decide this");

        public override int GetHashCode() => 0;

        public override string ToString() => "ThrowsOnEquals";
    }
}
